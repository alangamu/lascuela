using System;
using UnityEngine;

namespace Lascuela.Scripts.Interfaces
{
    public interface IMovementController
    {
        event Action OnStartWalk;
        event Action OnReachDestination;

        void SetDestination(Vector3 destinationPoint);
    }
}