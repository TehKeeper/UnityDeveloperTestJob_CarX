using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace General.Pooling.Makers {
    /// <summary> создаёт объект из префаба </summary>
    [Serializable]
    public class PrefabObjectMaker<T> : IObjectMaker<T> where T : UnityEngine.Component {
        [FormerlySerializedAs("m_prefab")] [SerializeField] private T _prefab;
        public T Prefab => _prefab;

        public T Make() => UnityEngine.Object.Instantiate(_prefab);

        public T MakeAtPoint(Transform point) =>
            UnityEngine.Object.Instantiate(_prefab, point.position, point.rotation);
    }
}