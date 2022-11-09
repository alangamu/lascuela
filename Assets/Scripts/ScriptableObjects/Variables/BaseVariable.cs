using System;
using UnityEngine;

namespace Lascuela.Scripts.ScriptableObjects.Variables
{
    public class BaseVariable<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public T Value => _value;

        public event Action<T> OnValueChanged;

        [SerializeField]
        private T _value;

        public void SetValue(T value)
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}