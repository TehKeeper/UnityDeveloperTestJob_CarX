using Logic.Monsters;
using UnityEngine;

namespace Logic.Towers {
    public abstract class BaseTower : MonoBehaviour {
        [Header("Tower stats")]
        [SerializeField] protected float _shootInterval = 0.5f;

        [SerializeField] protected float _range = 4f;

        [SerializeField] protected Transform _shootPoint;

        [SerializeField] protected float _shotTime;

        protected TargetFinder Radar;
        private bool _initialized;

        protected Monster Target;
        protected abstract Vector3 Interception { get; }

        private void Awake() {
            Radar = new TargetFinder(transform.position, _range * _range);

            if(InitializationCheck()){
                _initialized = true;
            }
            else {
                enabled = false;
            }

        }
        
        void Update() {
            if (!_initialized)
                return;

            if (SearchTarget()) return;

            FireProjectile();
        }
        
        private bool SearchTarget() {
            if (!Radar.IsValidTarget(Target, Interception)) {
                Radar.FindTarget(ref Target);
            }

            if (!Radar.TargetLocked)
                return true;
            return false;
        }

        protected abstract void FireProjectile();
        
        protected abstract bool InitializationCheck();
    }
}