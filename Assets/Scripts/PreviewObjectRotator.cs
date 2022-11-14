using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class PreviewObjectRotator : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _activeRoomObjectRotationIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _activeRoomObjectRotationIndex.SetValue((_activeRoomObjectRotationIndex.Value + 1) % 4);
            }
        }
    }
}