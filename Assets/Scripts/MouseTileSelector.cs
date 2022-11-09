using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Sets;
using Lascuela.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lascuela.Scripts
{
    public class MouseTileSelector : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask mouseColliderLayerMask;
        [SerializeField] 
        Box box;

        [SerializeField] 
        Collider[] selections;

        [SerializeField]
        private Camera cam;

        [SerializeField]
        private TileRuntimeSet _tileManager;

        [SerializeField]
        private BoolVariable _isShowingPreviewVariable;

        private List<ITile> tempSelections;
        private Vector3 startPos;
        private Vector3 dragPos;
        private Ray ray;

        private void Awake()
        {
            tempSelections = new List<ITile>();
        }

        private void Update()
        {
            if (_isShowingPreviewVariable.Value)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out RaycastHit hit, 999f, mouseColliderLayerMask);

                    
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            //on drag start
                            startPos = hit.point;
                            box.baseMin = startPos;
                        }
                        //when dragging the mouse
                        dragPos = hit.point;
                        box.baseMax = dragPos;

                        selections = Physics.OverlapBox(box.Center, box.Extents, Quaternion.identity);
                        foreach (var item in tempSelections)
                        {
                            item.SetRoomSelected(false);
                            item.ClearWallPreview();
                        }
                        foreach (var item in selections)
                        {
                            if (item.gameObject.TryGetComponent(out ITile tile))
                            {
                                tempSelections.Add(tile);
                                tile.SetRoomSelected(true);
                            }
                        }
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        //when mouse release
                        _isShowingPreviewVariable.SetValue(false);

                        foreach (var item in tempSelections)
                        {
                            item.SetRoomSelected(false);
                        }
                    }
                }
            }
        }
    }
}
