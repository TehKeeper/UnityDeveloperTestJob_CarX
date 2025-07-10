using UnityEngine;

namespace Logic.Towers {
    public abstract class BaseTower : MonoBehaviour {
        [Header("Tower stats")]
        [SerializeField] protected float m_shootInterval = 0.5f;

        [SerializeField] protected float m_range = 4f;

        [SerializeField] protected Transform m_shootPoint;

        [SerializeField] protected float _shotTime;
        
        private TargetFinder _radar;
        protected bool _initialized;

        protected Monster Target;
        protected abstract Vector3 Interception { get; }

        private void Awake() {
            _radar = new TargetFinder(transform.position, m_range * m_range);

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
            if (!_radar.IsValidTarget(Target, Interception)) {
                bool b = _radar.IsValidTarget(Target, Interception);
                _radar.FindTarget(ref Target);
            }

            if (!_radar.TargetLocked)
                return true;
            return false;
        }

        protected abstract void FireProjectile();
        
        protected abstract bool InitializationCheck();
    }
}