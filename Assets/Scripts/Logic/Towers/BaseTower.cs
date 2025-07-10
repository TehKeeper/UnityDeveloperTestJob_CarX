using General.Pooling;
using Logic.Monsters;
using Logic.Projectiles;
using UnityEngine;

namespace Logic.Towers {
    public abstract class BaseTower<T> : MonoBehaviour where T : BaseProjectile {
        [Header("Tower stats")]
        [SerializeField] protected float _shootInterval = 0.5f;

        [SerializeField] protected float _range = 4f;

        [SerializeField] protected Transform _shootPoint;

        [SerializeField] protected float _shotTime;

        protected TargetFinder Radar;
        private bool _initialized;

        protected Monster Target;
        protected abstract Vector3 Interception { get; }

        protected abstract ProjectilePoolBase<T> Pool { get; }

        private void Awake() {
            Radar = new TargetFinder(transform.position, _range * _range);

            if (!PoolAndPointCheck()) {
                _initialized = false;
                enabled = false;
            }

            Initialize();

            _initialized = true;
        }

        void Update() {
            if (!_initialized)
                return;

            if (!SearchTarget()) return;

            FireProjectile();
        }

        private bool SearchTarget() {
            if (!Radar.IsValidTarget(Target, Interception)) {
                Radar.FindTarget(ref Target);
            }

            return Radar.TargetLocked;
        }

        protected abstract void FireProjectile();

        /// <summary> Инициализация класса на Awake </summary>
        protected abstract void Initialize();

        private bool PoolAndPointCheck() {
            if (Pool == null) {
                Debug.Log("Проверьте пул объектов");
                enabled = false;
                return false;
            }

            if (_shootPoint == null) {
                Debug.Log("Проверьте точку выстрела");
                enabled = false;
                return false;
            }

            return true;
        }
    }
}