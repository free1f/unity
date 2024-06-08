using System;
using UnityEngine;
using UnityEngine.Events;

namespace Freelf.Patterns
{
    [Serializable]
    public class Observer<T>
    {
        [SerializeField]
        private T value;
        [SerializeField]
        private UnityEvent<T> onValueChanged = new ();
        public Observer(T value, UnityAction<T> callback = null)
        {
            this.value = value;
            if (callback != null) onValueChanged.AddListener(callback);
        }

        public T Value
        {
            get => value;
            set
            {   
                // TODO: Find where it is being initialized with the maxValue -- cause it's weird xd
                // if (Equals(this.value, value)) return;
                this.value = value;
                onValueChanged?.Invoke(this.value);     // Notify all listeners
            }
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (onValueChanged == null)
                onValueChanged = new UnityEvent<T>();
            onValueChanged.AddListener(callback);
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (onValueChanged == null) return;
            onValueChanged.RemoveListener(callback);
        }

        public void RemoveAllListeners()
        {
            if (onValueChanged == null) return;
            onValueChanged.RemoveAllListeners();
        }

        public void Dispose()
        {
            RemoveAllListeners();
            onValueChanged = null;
            value = default;
        }
    }
}