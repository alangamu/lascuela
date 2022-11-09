using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using System.Collections;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class RoomBuilderController : MonoBehaviour
    {
        [SerializeField]
        private RoomTypeGameEvent _roomTypeGameEvent;
        [SerializeField]
        private Transform _holder;

        private RoomTypeSO _roomTypeSO;

        private void OnEnable()
        {
            _roomTypeGameEvent.OnRaise += RoomTypeGameEventOnRaise;
        }

        private void RoomTypeGameEventOnRaise(RoomTypeSO roomType)
        {
            _roomTypeSO = roomType;
        }
    }
}