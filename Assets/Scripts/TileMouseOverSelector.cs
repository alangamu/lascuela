using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Sets;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class TileMouseOverSelector : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _isShowingPreviewVariable;
        [SerializeField]
        private BoolVariable _isMouseDraggingVariable;

        [SerializeField]
        private TileRuntimeSet _tileManager;

        private ITile _tile;
        private bool _isSelected;

        private void OnEnable()
        {
            TryGetComponent(out _tile);
            _isSelected = false;
        }

        private void OnMouseOver()
        {
            if (_isSelected)
            {
                return;
            }
            if (_isShowingPreviewVariable.Value)
            {
                _isSelected = true;
                _tileManager.SetFirstTilePreview(_tile);
                _tileManager.ShowRoomPreview(_tile);
            }
        }

        private void OnMouseEnter()
        {
            if (_isShowingPreviewVariable.Value)
            {
                if (_isMouseDraggingVariable.Value)
                {
                    _isSelected = true;
                    _tileManager.ShowRoomPreview(_tile);
                }
            }
        }

        private void OnMouseExit()
        {
            if (_isShowingPreviewVariable.Value)
            {
                if (!_isMouseDraggingVariable.Value)
                {
                    _isSelected = false;
                    _tile.SetRoomSelected(false);
                }
            }
        }
    }
}