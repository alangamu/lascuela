using Lascuela.Scripts.ScriptableObjects;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Lascuela.Scripts.UI
{
    public class RoomObjectPanelController : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;
        [SerializeField]
        private Transform _buttonsRoot;
        [SerializeField]
        private string _resourcesPath;
        [SerializeField]
        private GameObject _roomObjectButtonPrefab;
        [SerializeField]
        private RoomTypeVariable _activeRoomType;
        [SerializeField]
        private GameEvent _roomReadyToBuildEvent;
        [SerializeField]
        private GameEvent _buildRoomEvent;
        [SerializeField]
        private Button _buildButton;
        [SerializeField]
        private Button _cancelButton;

        public void HidePanel()
        {
            _root.gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            _root.gameObject.SetActive(true);
        }

        private void RoomReadyToBuildEventOnRaise()
        {
            _buildButton.interactable = true;
        }

        private void BuildRoom()
        {
            _buildRoomEvent.Raise();
            HidePanel();
        }

        private void Awake()
        {
            _buildButton.onClick.AddListener(BuildRoom);
            _cancelButton.onClick.AddListener(HidePanel);
        }

        private void Start()
        {
            ClearPanel();
            HidePanel();
        }

        private void ClearPanel()
        {
            foreach (Transform item in _buttonsRoot)
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
                    GameObject roomTypeButton = Instantiate(_roomObjectButtonPrefab, _buttonsRoot);

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
            _roomReadyToBuildEvent.OnRaise += RoomReadyToBuildEventOnRaise;
        }

        private void OnDisable()
        {
            _activeRoomType.OnValueChanged -= ActiveRoomTypeOnChanged;
            _roomReadyToBuildEvent.OnRaise -= RoomReadyToBuildEventOnRaise;
        }

        private void ActiveRoomTypeOnChanged(RoomTypeSO roomType)
        {
            ClearPanel();
            ShowPanel();
            FillPanel(roomType);
            _buildButton.interactable = false;
        }
    }
}