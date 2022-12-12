using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Events;
using Lascuela.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class StudentGeneratorManager : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _studentNumber;

        [SerializeField] 
        private GameEvent _generateStudentsEvent;

        private void OnEnable()
        {
            _generateStudentsEvent.OnRaise += GenerateStudentsOnRaise;
        }

        private void OnDisable()
        {
            _generateStudentsEvent.OnRaise -= GenerateStudentsOnRaise;
        }

        private void GenerateStudentsOnRaise()
        {
            for (int i = 0; i < _studentNumber.Value; i++)
            {
                GenerateStudent();
            }
        }

        private IStudent GenerateStudent()
        {
            IStudent student = new Student();
            student.Setup();
            return student;
        }
    }
}