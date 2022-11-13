using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private RoomObjectVariable _activeRoomObject;
        [SerializeField]
        private GameEvent _nextRoomObjectActivationEvent;
        [SerializeField]
        private IntVariable _gridSizeX;
        [SerializeField]
        private IntVariable _gridSizeZ;

        private GameObject _previewGameObject;
        private bool _isShowingObject = false;
        private bool _isHidingWall = false;
        private bool _isRotatingWithWall = false;
        private bool _isDoorObject = false;
        private ITile _tile;
        private IWallController _wallController;
        private int _doorRotationIndex = 0;

        private void OnEnable()
        {
            TryGetComponent(out _tile);
            TryGetComponent(out _wallController);

            _activeRoomObject.OnValueChanged += ActiveRoomObjectOnValueChanged;
        }

        private void OnDisable()
        {
            _activeRoomObject.OnValueChanged -= ActiveRoomObjectOnValueChanged;
        }

        private void ActiveRoomObjectOnValueChanged(RoomObjectSO activeRoomObject)
        {
            _isShowingObject = true;
            _isHidingWall = activeRoomObject.IsHidingWall;
            _isRotatingWithWall = activeRoomObject.IsRotatingWithWall;
            _isDoorObject = activeRoomObject.IsDoor;
            _doorRotationIndex = 0;
        }

        private void OnMouseOver()
        {
            if (_isShowingObject)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (_isRotatingWithWall)
                    {
                        RotateWithWall();
                    }
                    else
                    {
                        Rotate();
                    }

                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (_previewGameObject != null)
                    {
                        _previewGameObject = null;
                        _isShowingObject = false;
                        _nextRoomObjectActivationEvent.Raise();
                    }
                }
            }
        }

        private void OnMouseEnter()
        {
            if (!_tile.IsSelected)
            {
                return;
            }

            if (!_isShowingObject)
            {
                return;
            }

            if (_isRotatingWithWall)
            {
                if (!_tile.HasWall)
                {
                    return;
                }

                if (!IsShowingDoor())
                {
                    return;
                }

                //if (_wallController.Walls.Count == 1)
                //{
                //    if (_wallController.Walls[0] == 0 && _tile.Z == _gridSizeZ.Value - 1)
                //    {
                //        return;
                //    }

                //    if (_wallController.Walls[0] == 1 && _tile.X == _gridSizeX.Value - 1)
                //    {
                //        return;
                //    }
                //}

                //if (_wallController.Walls.Count == 2)
                //{

                //}

                _previewGameObject = Instantiate(_activeRoomObject.Value.RoomObjectPrefab, transform.position, Quaternion.identity);
                _tile.ShowDoorFramePreview();
                _doorRotationIndex = 0;
                RotateWithWall();
                return;
            }

            _previewGameObject = Instantiate(_activeRoomObject.Value.RoomObjectPrefab, transform.position, Quaternion.identity);
            Rotate();
        }

        private void OnMouseExit()
        {
            if (_isShowingObject)
            {
                if (_previewGameObject != null)
                {
                    Destroy(_previewGameObject);
                    if (_isHidingWall)
                    {
                        _tile.ShowWallPreview();
                    }
                }
            }
        }

        private bool IsShowingDoor()
        {
            List<int> walls = _wallController.Walls;

            switch (walls.Count)
            {
                case 1:
                    switch (walls[0])
                    {
                        case 0:
                            return !(_tile.Z == _gridSizeZ.Value - 1);
                        case 1:
                            return !(_tile.X == _gridSizeX.Value - 1);
                        case 2:
                            return !(_tile.Z == 0);
                        case 3:
                        default:
                            return !(_tile.X == 0);
                    }
                case 2:
                default:
                    switch (walls[0])
                    {
                        case 0:
                            return !(_tile.Z == _gridSizeZ.Value - 1 && ((walls[1] == 1 && _tile.X == _gridSizeX.Value - 1) || (walls[1] == 3 && _tile.X == 0)));
                        case 1:
                            return !(_tile.X == _gridSizeX.Value - 1 && walls[1] == 2 && _tile.Z == 0);
                        case 2:
                            return !(_tile.Z == 0 && walls[1] == 3 && _tile.X == 0);
                        case 3:
                        default:
                            return !(_tile.X == 0 && walls[1] == 0 && _tile.Z == _gridSizeZ.Value - 1);
                    }

            }
        }

        private void Rotate()
        {
            _previewGameObject.transform.rotation = Quaternion.identity;
            _previewGameObject.transform.Rotate(new Vector3(0f, (_doorRotationIndex++ % 4) * 90f, 0f));
        }

        private void RotateWithWall()
        {
            _previewGameObject.transform.rotation = Quaternion.identity;

            int rotationIndex = _wallController.Walls[_doorRotationIndex++ % _wallController.Walls.Count];

            print($"rotationIndex 1 {rotationIndex}");

            if (_isDoorObject)
            {
                bool canRotate;
                switch (rotationIndex)
                {
                    case 0:
                        canRotate = _tile.Z == _gridSizeZ.Value - 1;
                        break;
                    case 1:
                        canRotate = _tile.X == _gridSizeX.Value - 1;
                        break;
                    case 2:
                        canRotate = _tile.Z == 0;
                        break;
                    case 3:
                    default:
                        canRotate = _tile.X == 0;
                        break;
                }

                if (canRotate)
                {
                    rotationIndex = _wallController.Walls[_doorRotationIndex++ % _wallController.Walls.Count];
                }
            }

            if (_isHidingWall)
            {
                _tile.DoorFrameRotate(rotationIndex);
            }

            print($"rotationIndex 2 {rotationIndex}");

            _previewGameObject.transform.Rotate(new Vector3(0f, (rotationIndex) * 90f, 0f));
        }
    }

}