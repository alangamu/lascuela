using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class PreviewManager : MonoBehaviour
    {
        [SerializeField]
        private RoomTypeVariable _activeRoomType;
        [SerializeField]
        private BoolVariable _isShowingPreviewVariable;
        [SerializeField]
        private RoomObjectVariable _activeRoomObject;
        [SerializeField]
        private GameEvent _nextRoomObjectActivationEvent;
        [SerializeField]
        private GameEvent _roomReadyToBuildEvent;
        [SerializeField]
        private string _resourcesPath;

        private int _activeRoomObjectIndex;
        private Object[] _roomObjects;
        private List<RoomObjectSO> _roomTypeObjects;

        private void OnEnable()
        {
            _roomObjects = Resources.LoadAll(_resourcesPath, typeof(RoomObjectSO));
            _roomTypeObjects = new List<RoomObjectSO>();

            _activeRoomType.OnValueChanged += SetActiveRoomTypeEventOnRaise;
        }

        private void OnDisable()
        {
            _activeRoomType.OnValueChanged -= SetActiveRoomTypeEventOnRaise;
        }

        private void NextRoomObjectActivationEventOnRaise()
        {
            if (_roomTypeObjects.Count > _activeRoomObjectIndex)
            {
                _activeRoomObject.SetValue(_roomTypeObjects[_activeRoomObjectIndex]);
                _activeRoomObjectIndex++;
                return;
            }
            _roomReadyToBuildEvent.Raise();
            _nextRoomObjectActivationEvent.OnRaise -= NextRoomObjectActivationEventOnRaise;
        }

        private void SetActiveRoomTypeEventOnRaise(RoomTypeSO roomType)
        {
            _roomTypeObjects = new List<RoomObjectSO>();
            StartCoroutine(SetIsShowingPreview());
            _activeRoomObjectIndex = 0;
            _nextRoomObjectActivationEvent.OnRaise += NextRoomObjectActivationEventOnRaise;

            foreach (RoomObjectSO item in _roomObjects)
            {
                if (item.RoomTypes.Contains(_activeRoomType.Value))
                {
                    _roomTypeObjects.Add(item);
                }
            }
        }

        private IEnumerator SetIsShowingPreview()
        {
            yield return new WaitForSeconds(0.01f);
            _isShowingPreviewVariable.SetValue(true);
        }  

        private void Update()
        {
            if (_isShowingPreviewVariable.Value)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    //when mouse release
                    _isShowingPreviewVariable.SetValue(false);
                    NextRoomObjectActivationEventOnRaise();
                }
            }
        }
    }
}