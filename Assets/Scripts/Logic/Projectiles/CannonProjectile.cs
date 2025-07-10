using General.Pooling;
using UnityEngine;

namespace Logic.Projectiles {
    [RequireComponent(typeof(Rigidbody))]
    public class CannonProjectile : BaseProjectile {
        private Rigidbody _rb;
        private static readonly Vector3 V3Zero = Vector3.zero;

        protected override void Initialize() {
            _rb = Go.GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 velocity) {
            _rb.WakeUp();
            _rb.velocity = V3Zero;
            _rb.AddForce(velocity, ForceMode.Impulse);
        }


        protected override void ReturnToPool() {
            _rb.Sleep();
            CannonProjectilePool.Instance.ReturnToPool(this);
        }
    }
}