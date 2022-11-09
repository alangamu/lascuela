using Lascuela.Scripts.Interfaces;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class Desk : MonoBehaviour
    {
        [SerializeField]
        private Transform _forwardPoint;
        [SerializeField]
        private Transform _sitPoint;
        [SerializeField]
        private string _sittingAnimationName;

        GameObject _person;
        private IMovementController _personMovementController;

        public void SitPerson(GameObject person)
        {
            _person = person;
            if (_person.TryGetComponent(out _personMovementController))
            {
                _personMovementController.OnReachDestination += MovementControllerOnReachDestination;
                _personMovementController.SetDestination(_sitPoint.position);
            }
        }

        private void MovementControllerOnReachDestination()
        {
            _person.transform.LookAt(_forwardPoint);
            if (_person.TryGetComponent(out IAnimationController animationController))
            {
                animationController.PlayAnimation(_sittingAnimationName);
            }
            _personMovementController.OnReachDestination -= MovementControllerOnReachDestination;
        }

        private void OnDisable()
        {
            if (_personMovementController != null)
            {
                _personMovementController.OnReachDestination -= MovementControllerOnReachDestination;
            }
        }
    }
}