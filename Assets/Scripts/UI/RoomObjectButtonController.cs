using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

namespace Lascuela.Scripts.UI
{
    public class RoomObjectButtonController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _buttonName;

        [SerializeField]
        private RoomObjectVariable _activeRoomObject; 

        private RoomObjectSO _roomObject;

        public void Setup(string title, RoomObjectSO roomObject)
        {
            _buttonName.text = title;
            _roomObject = roomObject;
        }

        public void SetActiveRoomObject()
        {
            _activeRoomObject.SetValue(_roomObject);
        }
    }
}