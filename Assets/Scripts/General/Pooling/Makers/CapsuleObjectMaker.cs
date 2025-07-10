using System;
using UnityEngine;

namespace General.Pooling.Makers {
    [Serializable]
    public class CapsuleObjectMaker<T> : IObjectMaker<T> where T : UnityEngine.Component {
        private int _index = 0;
        public T Make() {
            GameObject newItem = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            newItem.name = $"{newItem.name}_{_index++}";
            Rigidbody itemRigidBody = newItem.AddComponent<Rigidbody>();
            itemRigidBody.useGravity = false;
            T component = newItem.AddComponent<T>();

            return component;
        }

        public T MakeAtPoint(Transform point) {
            T item = Make();
            item.transform.position = point.position;

            return item;
        }
    }
}