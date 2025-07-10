using System.Collections.Generic;
using General.Pooling.Makers;
using UnityEngine;

namespace General.Pooling {
    public abstract class ObjectPoolGeneric<T> : MonoBehaviour where T : UnityEngine.Component {
        [SerializeField] protected Transform PhysicalStorage;

        private Queue<T> _queue = new();
        private T _cachedObject;
        private Transform _cachedTransform;

        protected abstract IObjectMaker<T> ObjectMaker { get; }

        private void Awake() {
            if (PhysicalStorage == null) {
                PhysicalStorage = transform;
            }

            Intialize();
        }

        protected abstract void Intialize();

        public T GetAtPoint(Transform point) {
            if (_queue.Count == 0)
                return ObjectMaker.MakeAtPoint(point);

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