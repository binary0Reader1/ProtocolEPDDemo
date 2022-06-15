using Grid.TileFunctional;

namespace Grid
{
    /// <summary>
    /// Handles all grid tiles
    /// </summary>
    public class TilesHandler
    {
        private static bool isSetted = false;

        /// <summary>
        /// Initialises tiles handler.
        /// </summary>
        /// <param name="allTiles">All grid tiles.</param>
        public static void Setup(Tile[,] allTiles)
        {
            if (isSetted) return;

            Tiles = allTiles;
            Length = Tiles.GetLength(0);

            isSetted = true;
        }

        public static Tile[,] Tiles { get; private set; }
        public static int Length { get; private set; }
    }
}