using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Sets;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private StudentRuntimeSet _students;
        [SerializeField]
        private GameEvent _goToClassroomEvent;

        private void OnEnable()
        {
            _goToClassroomEvent.OnRaise += GoToClassroomOnRaise;
        }

        private void OnDisable()
        {
            _goToClassroomEvent.OnRaise -= GoToClassroomOnRaise;
        }

        private void GoToClassroomOnRaise()
        {
            _students.GoToClassroom();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Go To Classroom"))
            {
                _goToClassroomEvent.Raise();
            }

            if (GUILayout.Button("Go Sit"))
            {
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
    }
}