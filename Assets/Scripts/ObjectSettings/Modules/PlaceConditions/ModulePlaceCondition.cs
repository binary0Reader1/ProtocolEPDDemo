using FactoryObjects.TileObjects.Modules;
using Grid.TileFunctional;
using System.Collections.Generic;

namespace ObjectSettings.Modules.PlaceConditions
{
    /// <summary>
    /// Required for all module place conditions
    /// </summary>
    public abstract class ModulePlaceCondition
    {
        protected ModuleObject AttachedModuleObject { get; private set; }

        private Tile[] _conditionInvolvedTilemap;
        private TileCoordinate _savedModuleCoordinate;
        private bool _isInitialized = false;

        public void Initialize(ModuleObject attachedModuleObject)
        {
            if (_isInitialized) return;

            AttachedModuleObject = attachedModuleObject;
            InitializeInstruction();

            _isInitialized = true;
        }

        /// <summary>
        /// Returns array of involved tiles that not performed condition.
        /// </summary>
        /// <param name="moduleObject"></param>
        /// <returns></returns>
        public Tile[] GetNotPerformedConditionTiles(ModuleObject moduleObject)
        {
            Tile[] conditionInvolvedTiles = GetConditionInvolvedTilemap(moduleObject);

            List<Tile> notPerformedConditionTiles = new List<Tile>();

            foreach (Tile tile in conditionInvolvedTiles)
            {
                if (!IsContionPerformedByTile(tile)) notPerformedConditionTiles.Add(tile);
            }

            return notPerformedConditionTiles.ToArray();
        }

        /// <summary>
        /// Returns array of all tiles that involved in this condition.
        /// </summary>
        /// <param name="moduleObject"></param>
        /// <returns></returns>
        public Tile[] GetConditionInvolvedTilemap(ModuleObject moduleObject)
        {
            TileCoordinate newModuleCoordinate = new TileCoordinate(moduleObject.transform.position);  //Getting module object coordinate

            //This check is used to avoid repeated calculations of condition involved tiles
            if (_savedModuleCoordinate == null || _savedModuleCoordinate != newModuleCoordinate) //If saved coordinate is null or differs from new
            {
                _savedModuleCoordinate = newModuleCoordinate; //Rewriting saved module coordinate
                _conditionInvolvedTilemap = GetConditionInvolvedTileMapInstruction(moduleObject); //Calculate condition involved tiles with updated module coordinate
            }

            return _conditionInvolvedTilemap;
        }
        /// <summary>
        /// Used to initialize the class.
        /// </summary>
        protected abstract void InitializeInstruction();

        /// <summary>
        /// Describes how to get the tiles involved in the condition.
        /// </summary>
        /// <returns>Tiles involved in the condition.</returns>
        protected abstract Tile[] GetConditionInvolvedTileMapInstruction(ModuleObject moduleObject);

        /// <summary>
        /// Describes the check for the condition by the tile.
        /// </summary>
        /// <param name="tile">Checked tile.</param>
        /// <returns>True - if a tile fits the condition.</returns>
        protected abstract bool IsContionPerformedByTile(Tile tile);
    }
}