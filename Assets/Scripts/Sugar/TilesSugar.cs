using FactoryObjects.TileObjects;
using Grid;
using Grid.TileFunctional;
using System.Collections.Generic;
using UnityEngine;

namespace Sugar
{
    public static class TilesSugar
    {
        /// <summary>
        /// Returns all tiles occupied by the tile object with adjacent tiles.
        /// If a tile that exceeds the map bounds is requested during array generation, it will be not written.
        /// </summary>
        /// <param name="tileObject">Requested tile object.</param>
        /// <returns>Array of tiles.</returns>
        public static Tile[] GetTileObjectTilemapWithAdjacentTiles(TileObject tileObject) => GetTileObjectTilemapWithAdjacentTiles(tileObject, true);

        /// <summary>
        /// Returns all tiles occupied by the tile object with adjacent tiles.
        /// If during the formation of the array will be requested a tile that exceeds the boundaries of the map, an exception will be thrown.
        /// </summary>
        /// <param name="tileObject">Requested tile object.</param>
        /// <returns>Array of tiles.</returns>
        public static Tile[] GetTileObjectTilemapWithAdjacentTilesUNSAFE(TileObject tileObject) => GetTileObjectTilemapWithAdjacentTiles(tileObject, false);

        private static Tile[] GetTileObjectTilemapWithAdjacentTiles(TileObject tileObject, bool isSafe)
        {
            int extraTile = 1;

            int xCoordValue = Mathf.RoundToInt(tileObject.transform.position.x) - extraTile;
            int zCoordValue = Mathf.RoundToInt(tileObject.transform.position.z) - extraTile;

            int tileMapXLength = tileObject.XSize + extraTile * 2;
            int tileMapZLength = tileObject.ZSize + extraTile * 2;

            int xCoordBorder = tileMapXLength + xCoordValue;
            int zCoordBorder = tileMapZLength + zCoordValue;

            Tile[] tilemap = new Tile[tileMapXLength * tileMapZLength];

            if (isSafe)
            {
                return DefaultSafeFillAray(xCoordValue, xCoordBorder, zCoordValue, zCoordBorder);
            }

            return DefaultUnsafeFillAray(xCoordValue, xCoordBorder, zCoordValue, zCoordBorder);
        }






        /// <summary>
        /// Returns all tiles occupied by the tile object.
        /// If a tile that exceeds the map bounds is requested during array generation, it will be not written.
        /// </summary>
        /// <param name="tileObject">Requested tile object.</param>
        /// <returns>Array of tiles.</returns>
        public static Tile[] GetTileObjectTilemap(TileObject tileObject) => GetTileObjectTilemap(tileObject, true);

        /// <summary>
        /// Returns all tiles occupied by the tile object.
        /// If during the formation of the array will be requested a tile that exceeds the boundaries of the map, an exception will be thrown.
        /// </summary>
        /// <param name="tileObject">Requested tile object.</param>
        /// <returns>Array of tiles.</returns>
        public static Tile[] GetTileObjectTilemapUNSAFE(TileObject tileObject) => GetTileObjectTilemap(tileObject, false);

        private static Tile[] GetTileObjectTilemap(TileObject tileObject, bool isSafe)
        {
            int x = Mathf.RoundToInt(tileObject.transform.position.x);
            int z = Mathf.RoundToInt(tileObject.transform.position.z);

            int xCoordBorder = x + tileObject.XSize;
            int zCoordBorder = z + tileObject.ZSize;

            if (isSafe)
            {
                return DefaultSafeFillAray(x, xCoordBorder, z, zCoordBorder);
            }

            return DefaultUnsafeFillAray(x, xCoordBorder, z, zCoordBorder);
        }





        public static Tile[] GetTileMapFromStartPoint(TileCoordinate startTileCoordinate, int xSize, int zSize) => GetTileMapFromStartPoint(startTileCoordinate, xSize, zSize, true);
        public static Tile[] GetTileMapFromStartPointUNSAFE(TileCoordinate startTileCoordinate, int xSize, int zSize) => GetTileMapFromStartPoint(startTileCoordinate, xSize, zSize, false);
        private static Tile[] GetTileMapFromStartPoint(TileCoordinate startTileCoordinate, int xSize, int zSize, bool isSafe)
        {
            int x = Mathf.RoundToInt(startTileCoordinate.x);
            int z = Mathf.RoundToInt(startTileCoordinate.z);

            int xCoordBorder = x + xSize;
            int zCoordBorder = z + zSize;

            if (isSafe)
            {
                return DefaultSafeFillAray(x, xCoordBorder, z, zCoordBorder);
            }
            return DefaultUnsafeFillAray(x, xCoordBorder, z, zCoordBorder);
        }






        public static Tile[] GetTileMapFromCenter(TileCoordinate centreTileCoordinate, int xSize, int zSize) => GetTileMapFromCenter(centreTileCoordinate, xSize, zSize, true);
        public static Tile[] GetTileMapFromCenterUNSAFE(TileCoordinate centreTileCoordinate, int xSize, int zSize) => GetTileMapFromCenter(centreTileCoordinate, xSize, zSize, false);
        private static Tile[] GetTileMapFromCenter(TileCoordinate centreTileCoordinate, int xSize, int zSize, bool isSafe)
        {
            int middleOfXSizeFirstPart = xSize / 2;
            int middleOfXSizeSecondPart = xSize - middleOfXSizeFirstPart; //This is done to avoid problems with odd numbers

            int middleOfZSizeFirstPart = zSize / 2;
            int middleOfZSizeSecondPart = zSize - middleOfZSizeFirstPart; //This is done to avoid problems with odd numbers

            int x = centreTileCoordinate.x;
            int z = centreTileCoordinate.z;



            int xCoordValue = x - middleOfXSizeFirstPart;
            int zCoordValue = z - middleOfZSizeFirstPart;

            int xCoordBorder = x + middleOfXSizeSecondPart;
            int zCoordBorder = z + middleOfZSizeSecondPart;

            if (isSafe)
            {
                return DefaultSafeFillAray(xCoordValue, xCoordBorder, zCoordValue, zCoordBorder);
            }
            return DefaultUnsafeFillAray(xCoordValue, xCoordBorder, zCoordValue, zCoordBorder);
        }

        public static Tile[] GetTileMapFromCenterOfTileObject(TileObject tileObject, int xSize, int zSize) => GetTileMapFromCenterOfTileObject(tileObject, xSize, zSize, true);
        public static Tile[] GetTileMapFromCenterOfTileObjectUNSAFE(TileObject tileObject, int xSize, int zSize) => GetTileMapFromCenterOfTileObject(tileObject, xSize, zSize, false);
        private static Tile[] GetTileMapFromCenterOfTileObject(TileObject tileObject, int xSize, int zSize, bool isSafe)
        {
            int xOffset = Mathf.FloorToInt(tileObject.XSize / 2);
            //xOffset = xOffset <= 1 ? 0 : xOffset;

            int zOffset = Mathf.FloorToInt(tileObject.ZSize / 2);
            //zOffset = zOffset <= 1 ? 0 : zOffset;

            TileCoordinate functionalCenterOfObjectTileCoordinate = new TileCoordinate(tileObject.transform.position);

            TileCoordinate visualCenterOfObjectTileCoordinate = TilesHandler.Tiles
                    [functionalCenterOfObjectTileCoordinate.x + xOffset,
                    functionalCenterOfObjectTileCoordinate.z + zOffset].TileCoordinate;

            return GetTileMapFromCenter(visualCenterOfObjectTileCoordinate, xSize, zSize, isSafe);
        }



        /// <summary>
        /// Checks for location requested TileObject type on selected tiles.
        /// </summary>
        /// <typeparam name="T">Requested TileObject type.</typeparam>
        /// <param name="tilemap">Selected tiles.</param>
        /// <returns>True - if object located.</returns>
        public static bool IsObjectPlaced<T>(Tile[] tilemap) where T : TileObject
        {
            foreach (Tile tile in tilemap)
            {
                foreach (object placedObject in tile.PlacedObjects)
                {
                    if (placedObject is T)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static Tile[] DefaultSafeFillAray(int xCoordValue, int xCoordBorder, int zCoordValue, int zCoordBorder)
        {
            List<Tile> tileList = new List<Tile>();

            for (int xCoord = xCoordValue; xCoord < xCoordBorder; xCoord++)
            {
                for (int zCoord = zCoordValue; zCoord < zCoordBorder; zCoord++)
                {
                    if (xCoord < 0 || xCoord >= TilesHandler.Length || zCoord < 0 || zCoord >= TilesHandler.Length)
                    {
                        continue;
                    }

                    tileList.Add(TilesHandler.Tiles[xCoord, zCoord]);
                }
            }

            return tileList.ToArray();
        }

        private static Tile[] DefaultUnsafeFillAray(int xCoordValue, int xCoordBorder, int zCoordValue, int zCoordBorder)
        {
            List<Tile> tileList = new List<Tile>();

            for (int xCoord = xCoordValue; xCoord < xCoordBorder; xCoord++)
            {
                for (int zCoord = zCoordValue; zCoord < zCoordBorder; zCoord++)
                {
                    tileList.Add(TilesHandler.Tiles[xCoord, zCoord]);
                }
            }

            return tileList.ToArray();
        }
    }
}
