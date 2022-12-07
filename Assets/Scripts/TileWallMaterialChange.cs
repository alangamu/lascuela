using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileWallMaterialChange : MonoBehaviour, IMaterialChangeable
    {
        [SerializeField]
        private RoomTypeVariable _activeRoomType;

        [SerializeField]
        private MeshRenderer _doorFrameInteriorMesh;
        [SerializeField]
        private MeshRenderer _doorFrameExteriorMesh;

        [SerializeField]
        private MeshRenderer[] _interiorWallsMeshes;

        [SerializeField]
        private MeshRenderer[] _externalWallsMeshes;

        private Material _wallBaseMaterial;
        private ITile _tile;

        public void ChangeWallsMaterial(MeshRenderer[] wallsRenderer, Material wallMaterial)
        {
            foreach (MeshRenderer wallMesh in wallsRenderer)
            {
                wallMesh.material = wallMaterial;
            }
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _tile))
            {
                _tile.OnSetWallMaterial += TileOnSetWallMaterial;
            }

            _activeRoomType.OnValueChanged += ActiveRoomTypeChanged;
        }

        private void OnDisable()
        {
            _activeRoomType.OnValueChanged -= ActiveRoomTypeChanged;
            if (_tile != null)
            {
                _tile.OnSetWallMaterial -= TileOnSetWallMaterial;
            }
        }

        private void TileOnSetWallMaterial(Material wallMaterial)
        {
            _wallBaseMaterial = wallMaterial;

            ChangeWallsMaterial(_interiorWallsMeshes, _wallBaseMaterial);

            _doorFrameInteriorMesh.material = _wallBaseMaterial;
        }

        private void ActiveRoomTypeChanged(RoomTypeSO roomType)
        {
            if (!_tile.IsConstructed)
            {
                TileOnSetWallMaterial(roomType.RoomMaterial);
            }
        }
    }
}