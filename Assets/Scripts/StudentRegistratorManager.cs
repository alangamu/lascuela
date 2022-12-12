using Lascuela.Scripts.Interfaces;
using Lascuela.Scripts.ScriptableObjects.Sets;
using System.Collections;
using UnityEngine;

namespace Lascuela.Scripts
{
    public class StudentRegistratorManager : MonoBehaviour
    {
        [SerializeField]
        private StudentRuntimeSet _students;

        public void RegisterStudent(IStudent student)
        {
            _students.Add(student);
        }
    }
}