using Lascuela.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField]
        private int _gridSizeX = 22;
        [SerializeField]
        private int _gridSizeZ = 12;

        [SerializeField]
        private Vector3 _startingPosition;
        [SerializeField]
        private GameObject _gridTilePrefab;

        private void Start()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeZ; j++)
                {
                    Vector3 spawnPosition = new Vector3(-i * 2.5f, 0f, -j * 2.5f) + _startingPosition;
                    GameObject tileGameObject = Instantiate(_gridTilePrefab, spawnPosition, Quaternion.identity);
                    tileGameObject.name = $"Tile_{i}_{j}";
                    tileGameObject.transform.SetParent(transform);
                    if (tileGameObject.TryGetComponent(out ITile tile))
                    {
                        tile.Setup(i, j);
                        tile.SetRoomSelected(false);
                    }
                }
            }
        }
    }
}