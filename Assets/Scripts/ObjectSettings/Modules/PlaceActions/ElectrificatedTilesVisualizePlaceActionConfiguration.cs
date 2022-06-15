using GameSystems.Electrification;

namespace ObjectSettings.Modules.PlaceActions
{
    /// <summary>
    /// Displays the tile layout layer of all electrificated tiles.
    /// </summary>
    public class ElectrificatedTilesVisualizePlaceActionConfiguration : ModulePlaceActionConfiguration
    {
        private TileLayoutManagement _tileLayoutManagment;
        private ElectrificationSystem _electrificationSystem;
        public override void PlaceAction()
        {

        }

        public override void OnPlace()
        {
            _tileLayoutManagment.ClearTileLayout(TileLayoutType.Electrificated);
        }

        protected override void InitializeInstruction()
        {
            _tileLayoutManagment = UnityEngine.Object.FindObjectOfType<TileLayoutManagement>();
            _electrificationSystem = UnityEngine.Object.FindObjectOfType<ElectrificationSystem>();

            _tileLayoutManagment.SetTileLayout(TileLayoutType.Electrificated, _electrificationSystem.ElectrificatedTiles);
        }
    }
}