using General.Pooling;
using Logic.Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Towers {
    public class CannonTowerLinear : BaseTower<CannonProjectileLine> {
        [FormerlySerializedAs("_turret")]
        [Header("Turret Parts")]
        [SerializeField] private CannonTurret _turretHor;

        [SerializeField] private CannonTurret _turretVert;

        private float _projectileSpeed;
        private Vector3 _interception;
        protected override Vector3 Interception => _interception;
        
        private CannonProjectileLine _cachedProjectile;
        private bool _targetLocked;
        private Vector3 _targetPosition;
        protected override ProjectilePoolBase<CannonProjectileLine> Pool => CannonProjectileLinePool.Instance;

        protected override void Initialize() {
            _projectileSpeed = Pool.GetProjectileSpeed();
        }


        protected override void FireProjectile() {
            if (PreemptiveCalculator.TryCalculateInterception(_shootPoint.position, _projectileSpeed,
                    Target.Tf.position, Target.GetVelocity(), out _interception, out float time)) {
                _turretHor.RotateToTarget(_interception, RotationAxis.Y);
                _turretVert.RotateToTarget(_interception, RotationAxis.X);

                if (_shotTime > 0) {
                    _shotTime -= Time.deltaTime;
                    return;
                }


                _cachedProjectile = CannonProjectileLinePool.Instance.GetAtPoint(_shootPoint);
                _cachedProjectile.SetTranslation((_interception - _shootPoint.position).normalized);
                _shotTime = _shootInterval;
            }
        }
    }
}