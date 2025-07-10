using General.Pooling;
using UnityEngine;

namespace Logic.Projectiles {
    public class GuidedProjectile : BaseProjectile {
        private Transform _target;

        private Vector3 _cachedTranslation;
        private float _speedSquared;
        private Vector3 _interceptPoint;

        private void OnEnable() {
            _speedSquared = m_speed * m_speed;
        }

        void Update() {
            if (_target == null) {
                ReturnToPool();
                return;
            }

            _cachedTranslation = _target.position - Tf.position;
            if (_cachedTranslation.sqrMagnitude > _speedSquared) {
                _cachedTranslation = _cachedTranslation.normalized * m_speed;
            }

            Tf.Translate(_cachedTranslation * m_speed * Time.deltaTime);
        }


        protected override void ReturnToPool() {
            GuidedProjectilePool.Instance.ReturnToPool(this);
        }

        public void SetTarget(Transform targetTransform) => _target = targetTransform;
    }
}