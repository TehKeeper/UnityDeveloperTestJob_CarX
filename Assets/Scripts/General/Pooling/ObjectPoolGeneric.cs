using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

    public interface IObjectMaker<T> where T : UnityEngine.Component {
        public T Make();

        public T MakeAtPoint(Transform point);
    }

    [Serializable]
    public class PrefabObjectMaker<T> : IObjectMaker<T> where T : UnityEngine.Component {
        [SerializeField] private T m_prefab;
        public T Make() => UnityEngine.GameObject.Instantiate(m_prefab);

        public T MakeAtPoint(Transform point) =>
            UnityEngine.GameObject.Instantiate(m_prefab, point.position, point.rotation);
    }

    [Serializable]
    public class CapsuleObjectMaker<T> : IObjectMaker<T> where T : UnityEngine.Component {
        [SerializeField] private T m_prefab;

        public T Make() {
            GameObject newItem = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Rigidbody itemRigidBody = newItem.AddComponent<Rigidbody>();
            itemRigidBody.useGravity = false;
            T componenet = newItem.AddComponent<T>();

            return componenet;
        }

        public T MakeAtPoint(Transform point) {
            T item = Make();
            item.transform.position = point.position;

            return item;
        }
    }
}