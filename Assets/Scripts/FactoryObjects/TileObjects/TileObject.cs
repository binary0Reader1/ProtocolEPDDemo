using UnityEngine;
namespace FactoryObjects.TileObjects
{
    public abstract class TileObject : FactoryObject
    {
        public int XSize { get => _xSize; }
        public int ZSize { get => _zSize; }

        [SerializeField] protected int _xSize, _zSize = 1;
    }
}
