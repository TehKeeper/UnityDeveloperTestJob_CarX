using UnityEngine;
using General.Pooling;

[RequireComponent(typeof(Rigidbody))]
public class CannonProjectile : BaseProjectile {
    private Rigidbody _rb;

    protected override void Initialize() {
        _rb = Go.GetComponent<Rigidbody>();
      
    }

    public void Launch(Vector3 velocity) {
        _rb.AddForce(velocity, ForceMode.Impulse);
    }
    

    protected override void ReturnToPool() {
        _rb.Sleep();
        CannonProjectilePool.Instance.ReturnToPool(this);
    }
}