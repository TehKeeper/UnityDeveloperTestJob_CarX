using General.Pooling;
using Logic.Monsters;
using Logic.Projectiles;
using UnityEngine;

namespace Logic.Towers {
    /// <summary> Базовый класс для башень </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseTower<T> : MonoBehaviour where T : BaseProjectile {
        [Header("Tower stats")]
        [SerializeField] protected float _shootInterval = 0.5f;

        [SerializeField] protected float _range = 4f;

        [SerializeField] protected Transform _shootPoint;

        [SerializeField] protected float _shotTime;

        protected TargetFinder Radar;
        
        protected Monster Target;
        /// <summary> Точка пересечения снаряда и цели </summary>
        /// <info> Если снаряд самонаводящийся - можно использовать Target в качестве возвращаемого значения </info>
        protected abstract Vector3 Interception { get; }

        /// <summary> Пул Снарядов </summary>
        protected abstract ProjectilePoolBase<T> Pool { get; }
        
        private bool _initialized;

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

        /// <summary> Процесс стрельбы </summary>
        protected abstract void FireProjectile();

        /// <summary> Инициализация класса в Awake </summary>
        protected abstract void Initialize();

        /// <summary> Проверка наличия пула снарядов и точки ствола </summary>
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