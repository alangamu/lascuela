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
        private RoomTypeGameEvent _setActiveRoomTypeEvent;
        [SerializeField]
        private BoolVariable _isShowingPreviewVariable;
        [SerializeField]
        private RoomObjectVariable _activeRoomObject;
        [SerializeField]
        private GameEvent _nextRoomObjectActivationEvent;
        [SerializeField]
        private string _resourcesPath;


        private int _activeRoomObjectIndex;
        private RoomTypeSO _activeRoomType;
        private Object[] _roomObjects;
        private List<RoomObjectSO> _roomTypeObjects;

        private void OnEnable()
        {
            _roomObjects = Resources.LoadAll(_resourcesPath, typeof(RoomObjectSO));
            _roomTypeObjects = new List<RoomObjectSO>();

            _setActiveRoomTypeEvent.OnRaise += SetActiveRoomTypeEventOnRaise;
            _nextRoomObjectActivationEvent.OnRaise += NextRoomObjectActivationEventOnRaise;
        }

        private void OnDisable()
        {
            _setActiveRoomTypeEvent.OnRaise -= SetActiveRoomTypeEventOnRaise;
            _nextRoomObjectActivationEvent.OnRaise -= NextRoomObjectActivationEventOnRaise;
        }

        private void NextRoomObjectActivationEventOnRaise()
        {
            print($"_activeRoomObjectIndex {_activeRoomObjectIndex}");
            _activeRoomObject.SetValue(_roomTypeObjects[_activeRoomObjectIndex]);
            _activeRoomObjectIndex++;
        }

        private void SetActiveRoomTypeEventOnRaise(RoomTypeSO roomType)
        {
            //coroutine to prevent instant GetMouseButtonUp
            _activeRoomType = roomType;
            StartCoroutine(SetIsShowingPreview());
            _activeRoomObjectIndex = 0;

            foreach (RoomObjectSO item in _roomObjects)
            {
                if (item.RoomTypes.Contains(_activeRoomType))
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