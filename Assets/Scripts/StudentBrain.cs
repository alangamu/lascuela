using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Events;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class StudentBrain : MonoBehaviour

    {
        [SerializeField]
        private GameEvent _startClassEvent;
        [SerializeField]
        private GameEvent _endClassEvent;

        [SerializeField]
        private Desk _desk;

        public void SetDesk(Desk desk)
        {
            _desk = desk;
        }

        private void OnEnable()
        {
            _startClassEvent.OnRaise += StartClassEventOnRaise;
            _endClassEvent.OnRaise += EndClassEventOnRaise;
            //TryGetComponent(out _student);
        }

        private void OnDisable()
        {
            _startClassEvent.OnRaise -= StartClassEventOnRaise;
            _endClassEvent.OnRaise -= EndClassEventOnRaise;
        }

        private void EndClassEventOnRaise()
        {
            
        }

        private void StartClassEventOnRaise()
        {
            _desk.SitPerson(gameObject);
        }
    }
}