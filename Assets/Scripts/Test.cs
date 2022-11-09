using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private GameObject _kid;

        [SerializeField]
        private BoolVariable _isSowingPreview;

        [SerializeField]
        private Desk _desk;

        [SerializeField]
        private int _x;

        [SerializeField]
        private int _z;

        [SerializeField]
        private bool _isBuildingRight;

        [SerializeField]
        private bool _isBuildingUp;

        [SerializeField]
        private GameEvent _constructRoom;

        private IMovementController _movementController;

        private void OnEnable()
        {
            //_constructRoom.OnRaise += ConstructRoomOnRaise;
        }

        private void OnDisable()
        {
            //_constructRoom.OnRaise -= ConstructRoomOnRaise;
        }

        private void ConstructRoomOnRaise()
        {
            
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Start Walk"))
            {
                if (_kid.TryGetComponent(out _movementController))
                {
                    _movementController.SetDestination(_target.position);
                }
            }

            if (GUILayout.Button("Go Sit"))
            {
                _desk.SitPerson(_kid);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isSowingPreview.SetValue(!_isSowingPreview.Value);
            }
        }
    }
}