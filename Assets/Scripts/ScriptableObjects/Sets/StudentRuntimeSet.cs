using Lascuela.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects.Sets
{
    public class StudentRuntimeSet : RuntimeSet<IStudent>
    {
        public void GoToClassroom()
        {
            foreach (var student in Items)
            {
                //student.GoToClassroom();
            }
        }
    }
}