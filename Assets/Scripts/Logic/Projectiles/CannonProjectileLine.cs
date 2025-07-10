using General.Pooling;
using UnityEngine;

namespace Logic.Projectiles {
    public class CannonProjectileLine : BaseProjectile {
        private Vector3 _cachedTranslation;
    
        public void SetTranslation(Vector3 forward) {
            _cachedTranslation = forward;
        }

        protected override void Initialize() {
        
        }
    

        void Update() {
            Tf.Translate(_cachedTranslation * m_speed * Time.deltaTime, Space.World);
        }

        protected override void ReturnToPool() {
            CannonProjectileLinePool.Instance.ReturnToPool(this);
        }
    }
}