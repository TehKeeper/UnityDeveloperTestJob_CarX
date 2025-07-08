using General.Pooling;
using UnityEngine;

public class GuidedProjectile : BaseProjectile {
    public Transform m_target;

    private Vector3 _cachedTranslation;
    private float _speedSquared;

    private void OnEnable() {
        _speedSquared = m_speed * m_speed;
    }

    void Update() {
        if (m_target == null) {
            ReturnToPool();
            return;
        }

        _cachedTranslation = m_target.position - Tf.position;
        if (_cachedTranslation.sqrMagnitude > _speedSquared) {
            _cachedTranslation = _cachedTranslation.normalized * m_speed;
        }

        Tf.Translate(_cachedTranslation);
    }


    protected override void ReturnToPool() {
        GuidedProjectilePool.Instance.ReturnToPool(this);
    }
}