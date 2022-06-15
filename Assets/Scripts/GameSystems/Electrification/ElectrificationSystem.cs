using FactoryObjects.TileObjects;
using Grid.TileFunctional;
using Sugar;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Electrification
{
    /// <summary>
    /// Manages tiles electrification.
    /// </summary>
    public sealed class ElectrificationSystem : MonoBehaviour
    {
        public Action OnElectrificationListChanged;
        public Tile[] ElectrificatedTiles => _electrificatedTiles.ToArray();

        private List<IElectrifyEffect> _electrifyEffects = new List<IElectrifyEffect>();
        private List<Tile> _electrificatedTiles = new List<Tile>();

        /// <summary>
        /// Expands the electrification zone by ElectrifyEffect.
        /// </summary>
        public void AddElectrifyEffect(IElectrifyEffect electrifyEffect)
        {
            _electrifyEffects.Add(electrifyEffect);
            CalculateElectrificatedTiles();
            OnElectrificationListChanged?.Invoke();
        }

        /// <summary>
        /// Removes indicated ElectrifyEffect from electrification zone.
        /// </summary>
        public void RemoveElectrifyEffect(IElectrifyEffect electrifyEffect)
        {
            _electrifyEffects.Remove(electrifyEffect);
            CalculateElectrificatedTiles();
            OnElectrificationListChanged?.Invoke();
        }

        /// <summary>
        /// Returns true if indicated tile is electrificated.
        /// </summary>
        public bool IsTileElectrificated(Tile tile)
        {
            foreach (Tile electrificatedTile in _electrificatedTiles)
            {
                if (tile.TileCoordinate == electrificatedTile.TileCoordinate)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if all tiles under indicated tile object is electrificated.
        /// </summary>
        public bool IsTileObjectElectrificated(TileObject tileObject)
        {
            Tile[] tileObjectTileMap = TilesSugar.GetTileObjectTilemap(tileObject);
            foreach (Tile electrificatedTile in _electrificatedTiles)
            {
                foreach (Tile tileObjectTile in tileObjectTileMap)
                {
                    if (electrificatedTile.TileCoordinate == tileObjectTile.TileCoordinate)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates electrification zone by added electrify effects.
        /// </summary>
        private void CalculateElectrificatedTiles()
        {
            _electrificatedTiles.Clear();
            foreach (IElectrifyEffect electrifyEffect in _electrifyEffects)
            {
                TileObject attachedToElectrifyEffectTileObject = electrifyEffect.AttachedTileObjectReference;

                foreach (Tile tile in TilesSugar.GetTileMapFromCenterOfTileObject(attachedToElectrifyEffectTileObject, electrifyEffect.ElecrificationX, electrifyEffect.ElecrificationZ))
                {
                    _electrificatedTiles.Add(tile);
                }
            }
        }
    }
}

