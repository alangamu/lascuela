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
        private GameEvent _roomReadyToBuildEvent;
        [SerializeField]
        private IntVariable _gridSizeX;
        [SerializeField]
        private IntVariable _gridSizeZ;

        [SerializeField]
        private IntVariable _activeRoomObjectRotationIndex;

        private GameObject _previewGameObject;
        private bool _isShowingObject = false;
        private bool _isHidingWall = false;
        private bool _isRotatingWithWall = false;
        private bool _isDoorObject = false;
        private ITile _tile;
        private IWallController _wallController;
        private int _objectRotationIndex = 0;

        private void OnEnable()
        {
            TryGetComponent(out _tile);
            TryGetComponent(out _wallController);

            _activeRoomObject.OnValueChanged += ActiveRoomObjectOnValueChanged;
            _roomReadyToBuildEvent.OnRaise += RoomReadyToBuildEventOnRaise;
        }

        private void OnDisable()
        {
            _activeRoomObject.OnValueChanged -= ActiveRoomObjectOnValueChanged;
            _roomReadyToBuildEvent.OnRaise -= RoomReadyToBuildEventOnRaise;
        }

        private void RoomReadyToBuildEventOnRaise()
        {
            _isShowingObject = false;
        }

        private void ActiveRoomObjectRotationIndexOnValueChanged(int rotationIndex)
        {
            if (_isShowingObject)
            {
                if (_isRotatingWithWall)
                {
                    RotateWithWall();
                }
                else
                {
                    _objectRotationIndex = rotationIndex;
                    Rotate();
                }
            }
        }

        private void ActiveRoomObjectOnValueChanged(RoomObjectSO activeRoomObject)
        {
            _isShowingObject = true;
            _isHidingWall = activeRoomObject.IsHidingWall;
            _isRotatingWithWall = activeRoomObject.IsRotatingWithWall;
            _isDoorObject = activeRoomObject.IsDoor;
            _objectRotationIndex = 0;
        }

        private void OnMouseOver()
        {
            if (_isShowingObject)
            {
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
            _activeRoomObjectRotationIndex.OnValueChanged += ActiveRoomObjectRotationIndexOnValueChanged;

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

                _previewGameObject = Instantiate(_activeRoomObject.Value.RoomObjectPrefab, transform.position, Quaternion.identity);

                if (_isDoorObject)
                {
                    _tile.ShowDoorFramePreview();
                }
                _objectRotationIndex = 0;
                RotateWithWall();
                return;
            }

            _previewGameObject = Instantiate(_activeRoomObject.Value.RoomObjectPrefab, transform.position, Quaternion.identity);
            Rotate();
        }

        private void OnMouseExit()
        {
            _activeRoomObjectRotationIndex.OnValueChanged -= ActiveRoomObjectRotationIndexOnValueChanged;

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
            _previewGameObject.transform.Rotate(new Vector3(0f, _activeRoomObjectRotationIndex.Value * 90f, 0f));
        }

        private void RotateWithWall()
        {
            int rotationIndex = _wallController.Walls[_objectRotationIndex++ % _wallController.Walls.Count];

            _previewGameObject.transform.rotation = Quaternion.identity;

            if (_isDoorObject)
            {
                var canRotate = rotationIndex switch
                {
                    0 => _tile.Z == _gridSizeZ.Value - 1,
                    1 => _tile.X == _gridSizeX.Value - 1,
                    2 => _tile.Z == 0,
                    _ => _tile.X == 0,
                };
                if (canRotate)
                {
                    rotationIndex = _wallController.Walls[_objectRotationIndex++ % _wallController.Walls.Count];
                }
            }

            if (_isHidingWall)
            {
                _tile.DoorFrameRotate(rotationIndex);
            }

            _previewGameObject.transform.Rotate(new Vector3(0f, (rotationIndex) * 90f, 0f));
        }
    }
}