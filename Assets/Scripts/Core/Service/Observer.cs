using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player47.Core.Service
{
    [Serializable]
    public class Observer<T>
    {
        [SerializeField] T _value;
        [SerializeField] UnityEvent<T> _onValueChanged;

        public T Value
        {
            get => _value;
            set => Set(value);
        }

        public Observer(T value, UnityAction<T> callback = null)
        {
            this._value = value;

            _onValueChanged = new UnityEvent<T>();

            if (callback != null) _onValueChanged.AddListener(callback);
        }

        public void Set(T value)
        {
            if (Equals(_value, value)) return;

            this._value = value;

            Invoke();
        }

        public void Invoke()
        {
            Debug.Log($"Invoking {_onValueChanged.GetPersistentEventCount()} listeners");
            _onValueChanged?.Invoke(_value);
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) _onValueChanged = new UnityEvent<T>();

            _onValueChanged.AddListener(callback);
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) return;


            _onValueChanged.RemoveListener(callback);
        }

        public void RemoveAllListeners()
        {
            if (_onValueChanged == null) return;

            _onValueChanged.RemoveAllListeners();
        }

        public void Dispose()
        {
            RemoveAllListeners();
            _onValueChanged = null;
            Value = default;
        }
    }
}