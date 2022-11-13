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

        [SerializeField]
        private MeshRenderer[] _interiorWallsMeshes;

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
            foreach (MeshRenderer wallMesh in _interiorWallsMeshes)
            {
                wallMesh.material = roomType.RoomMaterial;
            }

            _doorFrameInteriorMesh.material = roomType.RoomMaterial;
            _doorFrameExteriorMesh.material = roomType.RoomMaterial;
        }
    }
}