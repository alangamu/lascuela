using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts.UI
{
    public class RoomObjectPanelController : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private string _resourcesPath;
        
        [SerializeField]
        private GameObject _roomObjectButtonPrefab;

        [SerializeField]
        private RoomTypeVariable _activeRoomType;

        public void HidePanel()
        {
            _root.gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            _root.gameObject.SetActive(true);
        }

        private void Start()
        {
            ClearPanel();
            HidePanel();
        }

        private void ClearPanel()
        {
            foreach (Transform item in _root)
            {
                Destroy(item.gameObject);
            }
        }

        private void FillPanel(RoomTypeSO roomType)
        {
            Object[] roomObjects = Resources.LoadAll(_resourcesPath, typeof(RoomObjectSO));

            foreach (RoomObjectSO item in roomObjects)
            {
                if (item.RoomTypes.Contains(roomType))
                {
                    GameObject roomTypeButton = Instantiate(_roomObjectButtonPrefab, _root);

                    if (roomTypeButton.TryGetComponent(out RoomObjectButtonController roomTypeButtonController))
                    {
                        roomTypeButtonController.Setup(item.name, item);
                    }
                }
            }
        }

        private void OnEnable()
        {
            _activeRoomType.OnValueChanged += ActiveRoomTypeOnChanged;
        }

        private void OnDisable()
        {
            _activeRoomType.OnValueChanged -= ActiveRoomTypeOnChanged;
        }

        private void ActiveRoomTypeOnChanged(RoomTypeSO roomType)
        {
            ClearPanel();
            ShowPanel();
            FillPanel(roomType);
        }
    }
}