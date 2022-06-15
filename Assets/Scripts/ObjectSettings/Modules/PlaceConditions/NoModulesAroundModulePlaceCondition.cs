using FactoryObjects.TileObjects;
using FactoryObjects.TileObjects.Modules;
using Grid.TileFunctional;
using Sugar;

namespace ObjectSettings.Modules.PlaceConditions
{
    /// <summary>
    /// Condition prohibiting building of a module if there are modules in adjacent tiles using this condition.
    /// </summary>
    public sealed class NoModulesAroundModulePlaceCondition : ModulePlaceCondition
    {
        protected override void InitializeInstruction()
        {
            
        }

        protected override Tile[] GetConditionInvolvedTileMapInstruction(ModuleObject moduleObject)
        {
            return TilesSugar.GetTileObjectTilemapWithAdjacentTiles(moduleObject);
        }

        protected override bool IsContionPerformedByTile(Tile tile)
        {
            foreach (TileObject tileObject in tile.PlacedObjects) //Pull out all objects placed on tile
            {
                if (tileObject is ModuleObject) //If there is module
                {
                    ModuleObject placedModuleObject = tileObject as ModuleObject;
                    foreach (ModulePlaceCondition placeCondition in placedModuleObject.ModulePlaceConditions) //Pull out all module's place conditions
                    {
                        if (placeCondition is NoModulesAroundModulePlaceCondition) //If there is this condition
                        {
                            return false; //Condition has been not performed
                        }
                    }
                }
            }

            return true;
        }
    }
}