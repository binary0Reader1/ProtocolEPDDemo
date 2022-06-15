using FactoryObjects.TileObjects.Modules;
using GameSystems.Electrification;
using Grid.TileFunctional;
using Sugar;
using UnityEngine;

namespace ObjectSettings.Modules.PlaceConditions
{
    /// <summary>
    /// Condition prohibiting building of a module if there are no electrificated tiles under module.
    /// </summary>
    public sealed class TilesElectrificatedPlaceCondition : ModulePlaceCondition
    {
        private ElectrificationSystem _electrificationSystem;
        protected override void InitializeInstruction()
        {
            _electrificationSystem = Object.FindObjectOfType<ElectrificationSystem>();
        }

        protected override Tile[] GetConditionInvolvedTileMapInstruction(ModuleObject moduleObject)
        {
            return TilesSugar.GetTileObjectTilemap(moduleObject);
        }

        protected override bool IsContionPerformedByTile(Tile tile)
        {
            bool isTileElectrificated = _electrificationSystem.IsTileElectrificated(tile);
            Debug.Log(isTileElectrificated);

            return isTileElectrificated;
        }
    }
}

