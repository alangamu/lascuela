using System;
using UnityEngine;

namespace Lascuela.Scripts.Interfaces
{
    public interface ITile
    {
        event Action OnShowWallPreview;
        event Action<Material> OnSetWallMaterial;
        event Action OnShowDoorPreview;
        event Action OnClearWallPreview;
        event Action<int> OnDoorFrameRotation;
        int X { get; }
        int Z { get; }
        bool IsSelected { get; }
        bool HasWall { get; }
        void Setup(int x, int z);
        void SetRoomSelected(bool select);
        void ShowWallPreview();
        void ShowDoorFramePreview();
        void ClearWallPreview();
        void SetHasWall(bool hasWall);
        void DoorFrameRotate(int rotationIndex);
        void SetWallMaterial(Material wallMaterial);
    }
}