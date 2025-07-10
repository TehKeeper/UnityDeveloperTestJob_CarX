using System.Collections.Generic;
using General.Pooling.Makers;
using UnityEngine;
using UnityEngine.Serialization;

namespace General.Pooling {
    /// <summary> Базовый класс для пулинга объектов </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectPoolGeneric<T> : MonoBehaviour where T : UnityEngine.Component {
        [FormerlySerializedAs("PhysicalStorage")] [SerializeField] protected Transform _physicalStorage;

        private readonly Queue<T> _queue = new();
        private T _cachedObject;
        private Transform _cachedTransform;

        /// <summary> Создаёт объекты </summary>
        protected abstract IObjectMaker<T> ObjectMaker { get; }

        private void Awake() {
            if (_physicalStorage == null) {
                _physicalStorage = transform;
            }

            Intialize();
        }

        protected abstract void Intialize();

        /// <summary> Получить объект в точке </summary>
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

      

        /// <summary> Вернуть в пул </summary>
        public void ReturnToPool(T item) {
            DisableItem(item);
            _queue.Enqueue(item);
        }

        /// <summary> Активировать объект после извлечение из пула </summary>
        protected abstract void EnableItem(T item);
        /// <summary> Деактивировать объект после возвращения в пул</summary>
        protected abstract void DisableItem(T item);
    }
}