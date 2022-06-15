using FactoryObjects.TileObjects;
using Grid.TileFunctional;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class TileDataManagment : MonoBehaviour
    {
        public void AddTilesData(Tile[] tilemap, TileObject tileObject)
        {
            foreach (Tile tile in tilemap)
            {
                //Debug.Log(tile.name);
                tile.AddTileData(tileObject);
            }
        }
        public void AddTileData(Tile tile, TileObject tileObject)
        {
            tile.AddTileData(tileObject);
        }

        public void RemoveTileData(Tile[,] tilemap, TileObject tileObject)
        {
            foreach (Tile tile in tilemap)
            {
                tile.AddTileData(tileObject);
            }
        }
    }
}
