using UnityEngine;

namespace General.Pooling.Makers {
    public interface IObjectMaker<T> where T : UnityEngine.Component {
        /// <summary> Создаёт объект </summary>
        public T Make();

        /// <summary> Создаёт объект в точке</summary>
        public T MakeAtPoint(Transform point);
    }
}