using System;
using System.Collections.Generic;
using UnityEngine;

namespace General.Pooling {
    public abstract class ObjectPoolGeneric<T> : MonoBehaviour where T: UnityEngine.Component {
        [SerializeField] private Transform _physicalStorage;

        [SerializeField] private T _prefab;

        private Queue<T> _queue = new();
        private T _cachedObject;
        private Transform _cachedTransform;

        private void Awake() {
            if (_physicalStorage == null) {
                _physicalStorage = transform;
            }

            Intialize();
        }

        protected abstract void Intialize();

        public T GetAtPoint(Transform point) {
            if (_queue.Count == 0) 
                return Instantiate(_prefab, point.position, point.rotation);

            _cachedObject = _queue.Dequeue();
            EnableItem(_cachedObject);
            _cachedTransform = _cachedObject.transform;
            _cachedTransform.position = point.position;
            _cachedTransform.rotation = point.rotation;

            return _cachedObject;
        }

        protected abstract void EnableItem(T item);

        public void ReturnToPool(T item) {
            DisableItem(item);
            _queue.Enqueue(item);
        }

        protected abstract void DisableItem(T item);
    }
}