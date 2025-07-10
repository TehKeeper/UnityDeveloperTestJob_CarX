using System;
using General.Pooling;
using Towers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Towers {
    public class CannonTower : BaseTower {
        [FormerlySerializedAs("_turret")]
        [Header("Turret Parts")]
        [SerializeField] private CannonTurret _turretHor;

        [SerializeField] private CannonTurret _turretVert;

        private float _projectileSpeed;
        private Vector3 _interception;
        protected override Vector3 Interception => _interception;
        private CannonProjectile _cachedProjectile;
        private bool _targetLocked;


        protected override bool InitializationCheck() {
            if (CannonProjectilePool.Instance == null || m_shootPoint == null) {
                Debug.Log("Проверьте пул объектов и/или точку выстрела");
                enabled = false;
                return false;
            }
            
            _projectileSpeed = CannonProjectilePool.Instance.GetProjectileSpeed();
            return true;
        }

        

        protected override void FireProjectile() {
            if (PreemptiveCalculator.TryCalculateInterception(m_shootPoint.position, _projectileSpeed,
                    Target.Tf.position, Target.GetVelocity(), out _interception, out float time)) {
                _turretHor.RotateToTarget(Target.Tf.position, RotationAxis.Y);


                if (PreemptiveCalculator.CalculateParabolicVelocity(m_shootPoint.position, _interception, time * 2f,
                        out Vector3 velocity, false)) {
                    _turretVert.RotateToDirection(velocity, RotationAxis.X);

                    if (_shotTime > 0) {
                        _shotTime -= Time.deltaTime;
                        return;
                    }


                    _cachedProjectile = CannonProjectilePool.Instance.GetAtPoint(m_shootPoint);
                    _cachedProjectile.Launch(velocity);
                    _shotTime = m_shootInterval;
                }
            }
        }
        
    }


    [Flags]
    public enum RotationAxis {
        X,
        Y,
        Z
    }
}