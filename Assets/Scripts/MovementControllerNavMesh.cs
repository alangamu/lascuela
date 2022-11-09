using Lascuela.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Lascuela.Scripts
{
    public class MovementControllerNavMesh : MonoBehaviour, IMovementController
    {
        public event Action OnStartWalk;
        public event Action OnReachDestination;

        private NavMeshAgent _navMeshAgent;
        private bool _isMoving = false;

        public void SetDestination(Vector3 destinationPoint)
        {
            _isMoving = true;
            _navMeshAgent.destination = destinationPoint;
            OnStartWalk?.Invoke();
        }

        private void Awake()
        {
            TryGetComponent(out _navMeshAgent);
        }

        private void Update()
        {
            if (_navMeshAgent.remainingDistance <= 0.1f && _isMoving)
            {
                OnReachDestination?.Invoke();
                _isMoving = false;
            }
        }
    }
}
