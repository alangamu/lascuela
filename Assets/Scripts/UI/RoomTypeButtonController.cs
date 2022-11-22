using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace Lascuela.Scripts.UI
{
    public class RoomTypeButtonController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _buttonName;

        [SerializeField]
        private RoomTypeVariable _activeRoomType;

        private RoomTypeSO _roomType;

        public void Setup(string title, RoomTypeSO roomType)
        {
            _buttonName.text = title;
            _roomType = roomType;
        }

        public void SetActiveRoomType()
        {
            _activeRoomType.SetValue(_roomType);
        }
    }
}