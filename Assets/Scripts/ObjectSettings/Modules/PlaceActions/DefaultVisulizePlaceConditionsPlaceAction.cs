using FactoryObjects.TileObjects.Modules;
using Grid.TileFunctional;
using ObjectSettings.Modules.PlaceConditions;
using System;
using System.Collections.Generic;

namespace ObjectSettings.Modules.PlaceActions
{
    /// <summary>
    /// Displays module's place conditions layout layer.
    /// </summary>
    public class DefaultVisulizePlaceConditionsPlaceAction : ModulePlaceActionConfiguration
    {
        private TileLayoutManagement _tileLayoutManagement;
        public override void PlaceAction()
        {
            Dictionary<Tile, bool> moduleConditionsMap = GetModuleConditionsMap(_attachedModuleObject);

            List<Tile> canBePlacedTiles = new List<Tile>();
            List<Tile> cantBePlacedTiles = new List<Tile>();

            foreach (KeyValuePair<Tile, bool> keyValuePair in moduleConditionsMap)
            {
                if (keyValuePair.Value)
                {
                    canBePlacedTiles.Add(keyValuePair.Key);
                    continue;
                }

                cantBePlacedTiles.Add(keyValuePair.Key);
            }

            _tileLayoutManagement.SetTileLayout(TileLayoutType.CanBePlaced, canBePlacedTiles.ToArray());
            _tileLayoutManagement.SetTileLayout(TileLayoutType.CantBePlaced, cantBePlacedTiles.ToArray());


        }

        public override void OnPlace()
        {
            _tileLayoutManagement.ClearTileLayout(TileLayoutType.CanBePlaced);
            _tileLayoutManagement.ClearTileLayout(TileLayoutType.CantBePlaced);
        }

        protected override void InitializeInstruction()
        {
            _tileLayoutManagement = UnityEngine.Object.FindObjectOfType<TileLayoutManagement>();
        }

        //FIX THIS:
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


