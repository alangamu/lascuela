using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Sets;
using System;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class Tile : MonoBehaviour, ITile
    {
        public event Action OnShowWallPreview;
        public event Action<Material> OnSetWallMaterial;
        public event Action OnClearWallPreview;
        public event Action OnShowDoorPreview;
        public event Action<int> OnDoorFrameRotation;

        public int X { get; private set; }
        public int Z { get; private set; }

        public bool IsSelected { get; private set; }

        public bool HasWall { get; private set; }

        public bool IsConstructed { get; private set; }

        [SerializeField]
        private TileRuntimeSet _tileManager;

        public void SetRoomSelected(bool select)
        {
            IsSelected = select;
        }

        public void Setup(int x, int z)
        {
            X = x;
            Z = z;
            HasWall = false;
        }

        public void ShowWallPreview()
        {
            OnShowWallPreview?.Invoke();
        }

        public void ClearWallPreview()
        {
            OnClearWallPreview?.Invoke();
        }

        public void SetHasWall(bool hasWall)
        {
            HasWall = hasWall;
        }

        public void ShowDoorFramePreview()
        {
            OnShowDoorPreview?.Invoke();
        }

        public void DoorFrameRotate(int rotationIndex)
        {
            OnDoorFrameRotation?.Invoke(rotationIndex);
        }

        public void SetWallMaterial(Material wallMaterial)
        {
            OnSetWallMaterial?.Invoke(wallMaterial);
        }

        private void Start()
        {
            ClearWallPreview();
            IsSelected = false;
        }

        private void OnEnable()
        {
            _tileManager.Add(this);
        }

        private void OnDisable()
        {
            _tileManager.Remove(this);
        }

        public void SetConstructed(bool constructed)
        {
            IsConstructed = constructed;
        }
    }
}