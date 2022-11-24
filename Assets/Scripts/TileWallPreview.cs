using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Sets;
using Lascuela.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileWallPreview : MonoBehaviour, IWallController
    {
        public List<int> Walls => _walls;

        [SerializeField]
        private GameObject _doorFrame;
        [SerializeField]
        private GameObject _tileDefault;
        [SerializeField]
        private GameObject _topWall;
        [SerializeField]
        private GameObject _bottomWall;
        [SerializeField]
        private GameObject _leftWall;
        [SerializeField]
        private GameObject _rightWall;
        [SerializeField]
        private GameObject _topRightPillar;
        [SerializeField]
        private GameObject _topLeftPillar;
        [SerializeField]
        private GameObject _bottomRightPillar;
        [SerializeField]
        private GameObject _bottomLeftPillar;
        [SerializeField]
        private TileRuntimeSet _tileManager;

        [SerializeField]
        private IntVariable _gridSizeX;
        [SerializeField]
        private IntVariable _gridSizeZ;

        private Material _wallMaterial;
        private ITile _tile;
        private List<int> _walls;

        private void RotateDoorFrame(int index)
        {
            _doorFrame.transform.rotation = Quaternion.identity;
            _doorFrame.transform.Rotate(new Vector3(0f, index * 90f, 0f));
            HideWall(index);
        }

        private void HideWall(int rotationIndex)
        {
            if (rotationIndex == 0)
            {
                _topWall.SetActive(false);
            }
            if (rotationIndex == 1)
            {
                _rightWall.SetActive(false);
            }
            if (rotationIndex == 2)
            {
                _bottomWall.SetActive(false);
            }
            if (rotationIndex == 3)
            {
                _leftWall.SetActive(false);
            }
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _tile))
            {
                _tile.OnClearWallPreview += ClearPreview;
                _tile.OnShowWallPreview += ShowWallPreview;
                _tile.OnShowDoorPreview += ShowDoorPreview;
                _tile.OnDoorFrameRotation += DoorFrameRotation;
            }
            _walls = new List<int>();
            _doorFrame.SetActive(false);
        }

        private void TileOnSetWallMaterial(Material wallMaterial)
        {
            
        }

        private void DoorFrameRotation(int rotationIndex)
        {
            ClearPreview();
            ShowWallPreview();
            _doorFrame.SetActive(true);
            RotateDoorFrame(rotationIndex);
        }

        private void ShowDoorPreview()
        {
            _doorFrame.SetActive(true);
            RotateDoorFrame(0);
        }

        private void OnDisable()
        {
            if (_tile != null)
            {
                _tile.OnClearWallPreview -= ClearPreview;
                _tile.OnShowWallPreview -= ShowWallPreview;
                _tile.OnShowDoorPreview -= ShowDoorPreview;
                _tile.OnDoorFrameRotation -= DoorFrameRotation;
            }
        }

        private void ShowWallPreview()
        {
            _walls.Clear();
            _doorFrame.SetActive(false);
            _topWall.SetActive(true);
            _bottomWall.SetActive(true);
            _leftWall.SetActive(true);
            _rightWall.SetActive(true);
            _topRightPillar.SetActive(true);
            _topLeftPillar.SetActive(true);
            _bottomRightPillar.SetActive(true);
            _bottomLeftPillar.SetActive(true);
            _walls.Add(0); //TOP
            _walls.Add(1); //RIGHT
            _walls.Add(2); //BOTTOM
            _walls.Add(3); //LEFT

            bool isTopTileSelected = _tileManager.IsTopTileSelected(_tile);
            bool isBottomTileSelected = _tileManager.IsBottomTileSelected(_tile);
            bool isLeftTileSelected = _tileManager.IsLeftTileSelected(_tile);
            bool isRightTileSelected = _tileManager.IsRightTileSelected(_tile);

            if (isTopTileSelected)
            {
                _walls.Remove(0);
                HideTopWall();
            }
            if (isBottomTileSelected)
            {
                _walls.Remove(2);
                HideBottomWall();
            }
            if (isLeftTileSelected)
            {
                _walls.Remove(3);
                HideLeftWall();
            }
            if (isRightTileSelected)
            {
                _walls.Remove(1);
                HideRightWall();
            }

            _tile.SetHasWall(_walls.Count > 0);

            CheckBorders();
        }

        private void CheckBorders()
        {
            if (_tile.Z == 0)
            {
                HideBottomWall();
            }

            if (_tile.Z == _gridSizeZ.Value - 1)
            {
                HideTopWall();
            }

            if (_tile.X == 0)
            {
                HideLeftWall();
            }

            if (_tile.X == _gridSizeX.Value - 1)
            {
                HideRightWall();
            }
        }

        private void HideRightWall()
        {
            _rightWall.SetActive(false);
            _topRightPillar.SetActive(false);
            _bottomRightPillar.SetActive(false);
        }

        private void HideLeftWall()
        {
            _leftWall.SetActive(false);
            _topLeftPillar.SetActive(false);
            _bottomLeftPillar.SetActive(false);
        }

        private void HideTopWall()
        {
            _topWall.SetActive(false);
            _topRightPillar.SetActive(false);
            _topLeftPillar.SetActive(false);
        }

        private void HideBottomWall()
        {
            _bottomWall.SetActive(false);
            _bottomRightPillar.SetActive(false);
            _bottomLeftPillar.SetActive(false);
        }

        private void ClearPreview()
        {
            _walls = new List<int>();
            _tile.SetHasWall(false);

            _topWall.SetActive(false);
            _bottomWall.SetActive(false);
            _leftWall.SetActive(false);
            _rightWall.SetActive(false);
            _topRightPillar.SetActive(false);
            _topLeftPillar.SetActive(false);
            _bottomRightPillar.SetActive(false);
            _bottomLeftPillar.SetActive(false);
        }
    }
}