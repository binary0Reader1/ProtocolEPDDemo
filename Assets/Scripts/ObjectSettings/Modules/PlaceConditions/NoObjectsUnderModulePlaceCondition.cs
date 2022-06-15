using FactoryObjects.TileObjects.Modules;
using Grid.TileFunctional;
using Sugar;

namespace ObjectSettings.Modules.PlaceConditions
{
    /// <summary>
    /// A condition prohibiting the building of a module if there are any objects under it.
    /// </summary>
    public sealed class NoObjectsUnderModulePlaceCondition : ModulePlaceCondition
    {
        protected override void InitializeInstruction()
        {
            
        }

        protected override Tile[] GetConditionInvolvedTileMapInstruction(ModuleObject moduleObject)
        {
            return TilesSugar.GetTileObjectTilemap(moduleObject);
        }

        protected override bool IsContionPerformedByTile(Tile tile)
        {
            if (tile.PlacedObjects.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}