using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace Lascuela.Scripts.UI
{
    public class RoomTypePanelController : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private GameEvent _showPanelEvent;

        [SerializeField]
        private GameObject _panelButtonPrefab;

        [SerializeField]
        private string _resourcesPath;

        public void HidePanel()
        {
            _root.gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            _root.gameObject.SetActive(true);
        }

        private void ClearPanel()
        {
            foreach (Transform item in _root)
            {
                Destroy(item.gameObject);
            }
        }

        private void OnEnable()
        {
            _showPanelEvent.OnRaise += ShowPanelEventOnRaise;
        }

        private void OnDisable()
        {
            _showPanelEvent.OnRaise -= ShowPanelEventOnRaise;
        }

        private void ShowPanelEventOnRaise()
        {
            ShowPanel();
        }

        private void Start()
        {
            ClearPanel();
            FillPanel();
            HidePanel();
        }

        private void FillPanel()
        {
            Object[] roomTypes = Resources.LoadAll(_resourcesPath, typeof(RoomTypeSO));

            foreach (RoomTypeSO item in roomTypes)
            {
                GameObject roomTypeButton = Instantiate(_panelButtonPrefab, _root);

                if (roomTypeButton.TryGetComponent(out RoomTypeButtonController roomTypeButtonController))
                {
                    roomTypeButtonController.Setup(item.name, item);
                }
            }
        }
    }
}