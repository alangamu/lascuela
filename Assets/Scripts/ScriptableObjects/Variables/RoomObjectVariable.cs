using System.Collections;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects.Variables
{
    [CreateAssetMenu(menuName = "Variables/Room Object Variable")]
    public class RoomObjectVariable : BaseVariable<RoomObjectSO>
    {
        private void OnEnable()
        {
            SetValue(null);
        }
    }
}