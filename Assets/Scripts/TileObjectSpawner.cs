using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private RoomObjectVariable _activeRoomObject;
        [SerializeField]
        private GameEvent _nextRoomObjectActivationEvent;

        private GameObject _previewGameObject;
        private bool _isShowingObject;
        private bool _isHidingWall;
        private bool _isRotatingWithWall;
        private ITile _tile;
        private IWallController _wallController;
        private int _doorRotationIndex;

        private void OnEnable()
        {
            TryGetComponent(out _tile);
            TryGetComponent(out _wallController);

            _activeRoomObject.OnValueChanged += ActiveRoomObjectOnValueChanged;
            _isShowingObject = false;
            _isHidingWall = false;
            _isRotatingWithWall = false;
            _doorRotationIndex = 0;
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
            _doorRotationIndex = 0;
        }

        private void OnMouseOver()
        {
            if (_isShowingObject)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _doorRotationIndex++;
                    if (_isRotatingWithWall)
                    {
                        int wallCount = _wallController.Walls.Count;
                        if (wallCount > 1)
                        {
                            if (_doorRotationIndex >= wallCount)
                            {
                                _doorRotationIndex = 0;
                            }

                            RotateWithWall(_doorRotationIndex);
                            if (_isHidingWall)
                            {
                                _tile.DoorFrameRotate();
                            }
                        }
                    }
                    else
                    {
                        if (_doorRotationIndex > 4)
                        {
                            _doorRotationIndex = 1;
                        }

                        Rotate(_doorRotationIndex);
                    }

                }
                if (Input.GetMouseButtonDown(0))
                {
                    _previewGameObject = null;
                    _isShowingObject = false;
                    _nextRoomObjectActivationEvent.Raise();
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

                _previewGameObject = Instantiate(_activeRoomObject.Value.RoomObjectPrefab, transform.position, Quaternion.identity);
                _tile.ShowDoorFramePreview();
                RotateWithWall(0);
                return;
            }

            _previewGameObject = Instantiate(_activeRoomObject.Value.RoomObjectPrefab, transform.position, Quaternion.identity);
            Rotate(0);
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

        private void Rotate(int index)
        {
            _previewGameObject.transform.rotation = Quaternion.identity;
            _previewGameObject.transform.Rotate(new Vector3(0f, (index) * 90f, 0f));
        }

        private void RotateWithWall(int index)
        {
            _previewGameObject.transform.rotation = Quaternion.identity;
            int rotationIndex = _wallController.Walls[index];
            _previewGameObject.transform.Rotate(new Vector3(0f, (rotationIndex - 1) * 90f, 0f));
        }
    }

}