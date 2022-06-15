using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid.TileFunctional;
using System;
using Object = UnityEngine.Object;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        public int GridSize { get => _gridSize; }

        [SerializeField] private int _gridSize = 30;
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private Floor _floorPrefab;

        private bool _testToggler = false;

        private void Awake()
        {
            Tile[,] allTiles = new Tile[_gridSize, _gridSize];

            for (int x = 0; x < _gridSize; x++)
            {
                for (int z = 0; z < _gridSize; z++)
                {
                    Tile tile = Instantiate(_tilePrefab, new Vector3(x, 0, z), _tilePrefab.transform.rotation, transform);
                    tile.name = $"Tile x{x}_z{z}";

                    allTiles[x, z] = tile;
                }
            }

            TilesHandler.Setup(allTiles);

            //CRUNCH ZONE
            int centerCoordOfGrid = _gridSize / 2;
            Instantiate(_floorPrefab, new Vector3(centerCoordOfGrid, _floorPrefab.transform.position.y, centerCoordOfGrid), Quaternion.identity);
            FindObjectOfType<CameraController>().Initialize(centerCoordOfGrid);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _testToggler = !_testToggler;

                foreach (Tile tile in TilesHandler.Tiles)
                {
                    tile.SetLayoutVisibility(_testToggler);
                }
            }
        }
    }
}
