using Grid.TileFunctional;
using Sugar;

namespace ObjectSettings.Modules.PlaceActions
{
    /// <summary>
    /// Displays the grid that the module will electrify after placement
    /// </summary>
    public sealed class ModuleElectrificationVisualizePlaceActionConfiguration : ModulePlaceActionConfiguration
    {
        private TileLayoutManagement _tileLayoutManagement;
        private int _xElectrification, _zElectrification;

        public ModuleElectrificationVisualizePlaceActionConfiguration(int xElectrification, int zElectrification)
        {
            _xElectrification = xElectrification;
            _zElectrification = zElectrification;
        }

        public override void PlaceAction()
        {
            Tile[] preElectrificationTiles = TilesSugar.GetTileMapFromCenterOfTileObject(_attachedModuleObject, _xElectrification, _zElectrification);
            _tileLayoutManagement.SetTileLayout(TileLayoutType.PreElectrfication, preElectrificationTiles);
        }
        public override void OnPlace()
        {
            _tileLayoutManagement.ClearTileLayout(TileLayoutType.PreElectrfication);
        }

        protected override void InitializeInstruction()
        {
            _tileLayoutManagement = UnityEngine.Object.FindObjectOfType<TileLayoutManagement>();
        }
    }
}