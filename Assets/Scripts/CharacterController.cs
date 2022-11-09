using Lascuela.Scripts.Interfaces;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class CharacterController : MonoBehaviour, ICharacterController
    {
        [SerializeField]
        private string _idleAnimationName;

        [SerializeField]
        private string _startWalkAnimationName;

        private IAnimationController _animationController;
        private IMovementController _movementController;

        private void OnEnable()
        {
            TryGetComponent(out _animationController);
            if (TryGetComponent(out _movementController))
            {
                _movementController.OnStartWalk += StartWalk;
                _movementController.OnReachDestination += ReachDestination;
            }
        }
        private void OnDisable()
        {
            if (_movementController != null)
            {
                _movementController.OnStartWalk -= StartWalk;
                _movementController.OnReachDestination -= ReachDestination;
            }
        }

        private void ReachDestination()
        {
            if (_animationController != null)
            {
                _animationController.PlayAnimation(_idleAnimationName);
            }
        }

        private void StartWalk()
        {
            if (_animationController != null)
            {
                _animationController.PlayAnimation(_startWalkAnimationName);
            }
        }
    }
}