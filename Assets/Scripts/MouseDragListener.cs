using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class MouseDragListener : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _isMouseDraggingVariable;

        private void Awake()
        {
            _isMouseDraggingVariable.SetValue(false);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //on drag start
                    if (!_isMouseDraggingVariable.Value)
                    {
                        _isMouseDraggingVariable.SetValue(true);
                    }
                }
                //when dragging the mouse
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //when mouse release
                _isMouseDraggingVariable.SetValue(false);
            }
        }
    }
}