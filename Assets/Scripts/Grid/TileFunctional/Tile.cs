using FactoryObjects.TileObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid.TileFunctional
{
    public class Tile : MonoBehaviour
    {
        public TileLayout TileLayoutReference => _tileLayout;
        public TileCoordinate TileCoordinate { get; private set; }
        public Queue<TileObject> PlacedObjects { get; private set; } = new Queue<TileObject>(1);

        [SerializeField] private TileLayout _tileLayout;

        public void SetLayoutVisibility(bool value)
        {
            _tileLayout.gameObject.SetActive(value);
        }

        public void AddTileData(TileObject tileObject)
        {
            PlacedObjects.Enqueue(tileObject);
        }

        public void RemoveTileData(TileObject tileObject)
        {
            PlacedObjects.Enqueue(tileObject);
        }

        private void Awake()
        {
            TileCoordinate = new TileCoordinate(transform.position);
        }
    }

    public class TileCoordinate
    {
        public static bool operator ==(TileCoordinate tileCoordinate1, TileCoordinate tileCoordinate2)
        {
            if (tileCoordinate1 is null || tileCoordinate2 is null)
            {
                if (tileCoordinate1 is null && tileCoordinate2 is null)
                {
                    return true;
                }

                if (tileCoordinate1 is null && tileCoordinate2 is not null)
                {
                    return false;
                }

                if (tileCoordinate1 is not null && tileCoordinate2 is null)
                {
                    return false;
                }
            }

            return tileCoordinate1.x == tileCoordinate2.x && tileCoordinate1.z == tileCoordinate2.z;
        }

        public static bool operator !=(TileCoordinate tileCoordinate1, TileCoordinate tileCoordinate2)
        {
            if (tileCoordinate1 is null || tileCoordinate2 is null)
            {
                if (tileCoordinate1 is null && tileCoordinate2 is null)
                {
                    return false;
                }

                if (tileCoordinate1 is null && tileCoordinate2 is not null)
                {
                    return true;
                }

                if (tileCoordinate1 is not null && tileCoordinate2 is null)
                {
                    return true;
                }
            }

            return tileCoordinate1.x != tileCoordinate2.x || tileCoordinate1.z != tileCoordinate2.z;
        }

        public TileCoordinate(Vector3 tilePosition)
        {
            x = Mathf.FloorToInt(tilePosition.x);
            z = Mathf.FloorToInt(tilePosition.z);
        }

        public int x { get; private set; }
        public int z { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is TileCoordinate coordinate &&
                   x == coordinate.x &&
                   z == coordinate.z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, z);
        }
    }
}
