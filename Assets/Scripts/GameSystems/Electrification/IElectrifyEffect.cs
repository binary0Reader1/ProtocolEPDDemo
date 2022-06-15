using FactoryObjects.TileObjects;

namespace GameSystems.Electrification
{
    public interface IElectrifyEffect
    {
        public int ElecrificationX { get; set; }
        public int ElecrificationZ { get; set; }
        public TileObject AttachedTileObjectReference { get; set; }
    }
}
