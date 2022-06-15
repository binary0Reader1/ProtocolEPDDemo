using FactoryObjects.TileObjects;
using FactoryObjects.TileObjects.Modules;
using Grid;
using Grid.TileFunctional;
using ObjectSettings.Modules.PlaceConditions;
using Sugar;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Operations
{
    public class ModuleBuildingOperation : MonoBehaviour
    {
        private TileDataManagment _tileDataManagment;
        private TileLayoutManagement _tileLayoutManagment;

        private ModuleObject _placedBuilding;
        private Camera _mainCamera;

        private bool _canBePlaced = false;

        private List<ModuleObject> _modifiedModulesCRUNCHED = new List<ModuleObject>();


        private void Awake()
        {
            _tileDataManagment = FindObjectOfType<TileDataManagment>();
            _tileLayoutManagment = FindObjectOfType<TileLayoutManagement>();

            _mainCamera = Camera.main;
        }

        public void StartPlacingModule(ModuleObject buildingPrefab)
        {
            if (_placedBuilding != null)
            {
                Destroy(_placedBuilding);
            }

            _placedBuilding = Instantiate(buildingPrefab);
            _placedBuilding.StartPlacing();

            _placedBuilding.SetMiddleTransparentModeCRUNCHED();
        }

        public void Update()
        {
            //If the value is not zero, it means that StartPlacingModule() function has been called 
            if (_placedBuilding != null)
            {
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    try
                    {
                        Vector3 worldPosition = ray.GetPoint(position); //Mouse position

                        int x = Mathf.RoundToInt(worldPosition.x);
                        int z = Mathf.RoundToInt(worldPosition.z);

                        if (x != _placedBuilding.transform.position.x || z != _placedBuilding.transform.position.z) //If mouse position has been changed
                        {
                            _placedBuilding.transform.position = new Vector3(x, 0, z);

                            _placedBuilding.PlaceAction();

                            _canBePlaced = CanBePlaced(); //Calculating the possibility of placing the module on mouse position tiles
                            VisualizeModuleConditionsTileMapCRUNCHED();
                        }

                        if (Input.GetMouseButtonDown(0) && _canBePlaced)  //Create module
                        {
                            Tile[] tileMapWithNoAdjacentTiles = TilesSugar.GetTileObjectTilemapUNSAFE(_placedBuilding);
                            _tileDataManagment.AddTilesData(tileMapWithNoAdjacentTiles, _placedBuilding); //Writing data about module

                            PlacedCRUNCHED();

                            _placedBuilding.Place();
                            _placedBuilding = null;
                        }

                        if (Input.GetMouseButtonDown(1))  //Cancel module
                        {
                            PlacedCRUNCHED();
                            Destroy(_placedBuilding.gameObject);
                        }
                    }

                    catch (IndexOutOfRangeException)
                    {
                        //OutOfGrid();
                        return;
                    }
                }
            }
        }

        private bool CanBePlaced()
        {
            foreach (ModulePlaceCondition modulePlaceCondition in _placedBuilding.ModulePlaceConditions)
            {
                if (modulePlaceCondition.GetNotPerformedConditionTiles(_placedBuilding).Length != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if a module can be built on the current tiles.
        /// </summary>
/*        private bool CanBePlaced(Dictionary<Tile, bool> moduleConditionsMap)
        {
            if (moduleConditionsMap == null)
            {
                return false;
            }

            foreach (KeyValuePair<Tile, bool> keyValuePair in moduleConditionsMap)
            {
                if (keyValuePair.Value == false)
                {
                    return false;
                }
            }
            return true;
        }*/

        private void PlacedCRUNCHED()
        {
            foreach (var obj in _modifiedModulesCRUNCHED)
            {
                obj.SetDefaultModeCRUNCHED();
            }

            _placedBuilding.SetDefaultModeCRUNCHED();
            //_tileLayoutManagment.ClearAllTileLayouts();
        }

        private void OutOfGrid()
        {
            _tileLayoutManagment.ClearAllTileLayouts();
        }

        private void VisualizeModuleConditionsTileMapCRUNCHED()
        {
            foreach (var obj in _modifiedModulesCRUNCHED)
            {
                obj.SetDefaultModeCRUNCHED();
            }

            foreach (ModulePlaceCondition placeCondition in _placedBuilding.ModulePlaceConditions)
            {
                foreach (Tile tile in placeCondition.GetConditionInvolvedTilemap(_placedBuilding))
                {
                    if (tile.PlacedObjects.Count != 0)
                    {
                        foreach (TileObject tileObject in tile.PlacedObjects)
                        {
                            if (tileObject is ModuleObject)
                            {
                                ModuleObject moduleObject = tileObject as ModuleObject;
                                moduleObject.SetTransparentModeCRUNCHED();
                                _modifiedModulesCRUNCHED.Add(moduleObject);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Forms list of tiles with their conditions result.
        /// </summary>
        /// <returns>Key - involved in condition tile, value - tile condition pass result.</returns>
        private Dictionary<Tile, bool> GetModuleConditionsMap(ModuleObject moduleObject)
        {
            try
            {
                HashSet<Tile> allNotPerformedConditionsTiles = new HashSet<Tile>(); //A collection storing all tiles
                                                                                    //that did not pass one of conditions in a single copy

                HashSet<Tile> allConditionsInvolvedTiles = new HashSet<Tile>(); //A collection storing all tiles
                                                                                //that involved in one of conditions in a single copy

                foreach (ModulePlaceCondition placeCondition in moduleObject.ModulePlaceConditions) //Pull out all module place conditions
                {
                    foreach (Tile notPerformedConditionTile in placeCondition.GetNotPerformedConditionTiles(moduleObject)) //Getting all tiles that did not pass the one of conditions
                    {
                        allNotPerformedConditionsTiles.Add(notPerformedConditionTile);
                    }

                    foreach (Tile conditionInvolvedTile in placeCondition.GetConditionInvolvedTilemap(moduleObject)) //Getting all tiles that involved in conditions
                    {
                        allConditionsInvolvedTiles.Add(conditionInvolvedTile);
                    }
                }

                Dictionary<Tile, bool> ConditionsMap = new Dictionary<Tile, bool>(); //A condition map that will be returned

                foreach (Tile tile in allConditionsInvolvedTiles) //Pull out all involved in conditions tiles
                {
                    bool isNotPerformedConditionTile = false;
                    foreach (Tile notPerformedConditionsTile in allNotPerformedConditionsTiles) //Pull out all tiles that did not pass one of conditions
                    {
                        if (tile.TileCoordinate == notPerformedConditionsTile.TileCoordinate) //If the cell is one of those that did not pass one of condition
                        {
                            isNotPerformedConditionTile = true;
                            allNotPerformedConditionsTiles.Remove(notPerformedConditionsTile);
                            break;
                        }
                    }
                    if (!isNotPerformedConditionTile)
                    {
                        ConditionsMap.Add(tile, true); //Mark the tile as buildable
                    }
                    else
                    {
                        ConditionsMap.Add(tile, false); //Mark the tile as non buildable
                    }
                }

                return ConditionsMap;
            }

            catch (IndexOutOfRangeException)
            {

                return null;
            }
        }
    }
}
