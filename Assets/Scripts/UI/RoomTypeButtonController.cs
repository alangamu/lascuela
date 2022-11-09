using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using TMPro;
using UnityEngine;

namespace Lascuela.Scripts.UI
{
    public class RoomTypeButtonController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _buttonName;

        [SerializeField]
        private RoomTypeGameEvent _setActiveRoomTypeEvent;

        private RoomTypeSO _roomType;

        public void Setup(string title, RoomTypeSO roomType)
        {
            _buttonName.text = title;
            _roomType = roomType;
        }

        public void SetActiveRoomType()
        {
            _setActiveRoomTypeEvent.Raise(_roomType);
        }
    }
}