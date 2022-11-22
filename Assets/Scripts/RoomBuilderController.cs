using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class RoomBuilderController : MonoBehaviour
    {
        [SerializeField]
        private RoomTypeVariable _activeRoomType;
        [SerializeField]
        private Transform _holder;

        private RoomTypeSO _roomTypeSO;

        private void OnEnable()
        {
            _activeRoomType.OnValueChanged += ActiveRoomTypeOnChanged;
        }

        private void ActiveRoomTypeOnChanged(RoomTypeSO roomType)
        {
            _roomTypeSO = roomType;
        }
    }
}