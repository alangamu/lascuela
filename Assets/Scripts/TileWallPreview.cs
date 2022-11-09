using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Sets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileWallPreview : MonoBehaviour, IWallController
    {
        public List<int> Walls => _walls;

        [SerializeField]
        private Material _previewMaterial;
        [SerializeField]
        private Material _normalMaterial;
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

        private Renderer _renderer;
        private ITile _tile;
        private List<int> _walls;
        private int _doorFrameRotationIndex;

        private void RotateDoorFrame(int index)
        {
            _doorFrame.transform.rotation = Quaternion.identity;
            int rotationIndex = _walls[index];
            _doorFrame.transform.Rotate(new Vector3(0f, (rotationIndex - 1) * 90f, 0f));
            HideWall(rotationIndex);
        }

        private void HideWall(int rotationIndex)
        {
            if (rotationIndex == 1)
            {
                _topWall.SetActive(false);
            }
            if (rotationIndex == 2)
            {
                _rightWall.SetActive(false);
            }
            if (rotationIndex == 3)
            {
                _bottomWall.SetActive(false);
            }
            if (rotationIndex == 4)
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
            _tileDefault.TryGetComponent(out _renderer);
            _walls = new List<int>();
            _doorFrameRotationIndex = 0;
            _doorFrame.SetActive(false);
        }

        private void DoorFrameRotation()
        {
            _doorFrameRotationIndex++;
            if (_doorFrameRotationIndex >= _walls.Count)
            {
                _doorFrameRotationIndex = 0;
            }
            ClearPreview();
            ShowWallPreview();
            _doorFrame.SetActive(true);
            RotateDoorFrame(_doorFrameRotationIndex);
        }

        private void ShowDoorPreview()
        {
            _doorFrame.SetActive(true);
            RotateDoorFrame(_doorFrameRotationIndex);
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
            _renderer.material = _previewMaterial;
            _topWall.SetActive(true);
            _bottomWall.SetActive(true);
            _leftWall.SetActive(true);
            _rightWall.SetActive(true);
            _topRightPillar.SetActive(true);
            _topLeftPillar.SetActive(true);
            _bottomRightPillar.SetActive(true);
            _bottomLeftPillar.SetActive(true);
            _walls.Add(1); //TOP
            _walls.Add(2); //RIGHT
            _walls.Add(3); //BOTTOM
            _walls.Add(4); //LEFT

            bool isTopTileSelected = _tileManager.IsTopTileSelected(_tile);
            bool isBottomTileSelected = _tileManager.IsBottomTileSelected(_tile);
            bool isLeftTileSelected = _tileManager.IsLeftTileSelected(_tile);
            bool isRightTileSelected = _tileManager.IsRightTileSelected(_tile);

            if (isTopTileSelected)
            {
                _walls.Remove(1);
                _topWall.SetActive(false);
                _topRightPillar.SetActive(false);
                _topLeftPillar.SetActive(false);
            }
            if (isBottomTileSelected)
            {
                _walls.Remove(3);
                _bottomWall.SetActive(false);
                _bottomRightPillar.SetActive(false);
                _bottomLeftPillar.SetActive(false);
            }
            if (isLeftTileSelected)
            {
                _walls.Remove(4);
                _leftWall.SetActive(false);
                _topLeftPillar.SetActive(false);
                _bottomLeftPillar.SetActive(false);
            }
            if (isRightTileSelected)
            {
                _walls.Remove(2);
                _rightWall.SetActive(false);
                _topRightPillar.SetActive(false);
                _bottomRightPillar.SetActive(false);
            }

            _tile.SetHasWall(_walls.Count > 0);
        }

        private void ClearPreview()
        {
            _renderer.material = _normalMaterial;
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