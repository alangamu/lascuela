using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileWallMaterialChange : MonoBehaviour
    {
        [SerializeField]
        private RoomTypeGameEvent _setActiveRoomTypeEvent;

        [SerializeField]
        private MeshRenderer _doorFrameInteriorMesh;
        [SerializeField]
        private MeshRenderer _doorFrameExteriorMesh;

        private void OnEnable()
        {
            _setActiveRoomTypeEvent.OnRaise += SetActiveRoomTypeEventOnRaise;
        }

        private void OnDisable()
        {
            _setActiveRoomTypeEvent.OnRaise -= SetActiveRoomTypeEventOnRaise;
        }

        private void SetActiveRoomTypeEventOnRaise(RoomTypeSO roomType)
        {
            _doorFrameInteriorMesh.material = roomType.RoomMaterial;
            _doorFrameExteriorMesh.material = roomType.RoomMaterial;
        }
    }
}