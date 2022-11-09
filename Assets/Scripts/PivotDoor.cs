using Lascuela.Scripts.Interfaces;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class PivotDoor : MonoBehaviour, IDoor, IActivable
    {
        [SerializeField]
        private GameObject _doorMesh;
        [SerializeField]
        private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            Open();
        }

        private void OnTriggerExit(Collider other)
        {
            Close();
        }

        public void Close()
        {
            _doorMesh.transform.Rotate(new Vector3(0, -90f, 0f));
        }

        public void Open()
        {
            _doorMesh.transform.Rotate(new Vector3(0, 90f, 0f));
        }

        public void Activate()
        {
            _collider.enabled = true;
        }
    }
}