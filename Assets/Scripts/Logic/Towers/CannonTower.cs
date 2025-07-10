using General.Pooling;
using Logic.Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Towers {
    public class CannonTower : BaseTower<CannonProjectile> {
        [FormerlySerializedAs("_turret")]
        [Header("Turret Parts")]
        [SerializeField] private CannonTurret _turretHor;

        [SerializeField] private CannonTurret _turretVert;

        private float _projectileSpeed;
        private Vector3 _interception;
        protected override Vector3 Interception => _interception;
        protected override ProjectilePoolBase<CannonProjectile> Pool => CannonProjectilePool.Instance;
        private CannonProjectile _cachedProjectile;
        private bool _targetLocked;


        protected override void Initialize() {
            _projectileSpeed = CannonProjectilePool.Instance.GetProjectileSpeed();
        }
        
        protected override void FireProjectile() {
            if (PreemptiveCalculator.TryCalculateInterception(_shootPoint.position, _projectileSpeed,
                    Target.Tf.position, Target.GetVelocity(), out _interception, out float time)) {
                _turretHor.RotateToTarget(_interception, RotationAxis.Y);


                if (PreemptiveCalculator.CalculateParabolicVelocity(_shootPoint.position, _interception, time * 2f,
                        out Vector3 velocity, false)) {
                    _turretVert.RotateToDirection(velocity, RotationAxis.X);

                    if (_shotTime > 0) {
                        _shotTime -= Time.deltaTime;
                        return;
                    }


                    _cachedProjectile = CannonProjectilePool.Instance.GetAtPoint(_shootPoint);
                    _cachedProjectile.Launch(velocity);
                    _shotTime = _shootInterval;
                }
            }
        }
    }
}