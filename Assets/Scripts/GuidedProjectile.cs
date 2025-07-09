using System;
using General.Pooling;
using Towers;
using UnityEngine;

public class GuidedProjectile : BaseProjectile {
    public Transform m_target;

    private Vector3 _cachedTranslation;
    private float _speedSquared;
    private Vector3 _interceptPoint;

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

        Tf.Translate(_cachedTranslation * m_speed * Time.deltaTime);
    }


    protected override void ReturnToPool() {
        GuidedProjectilePool.Instance.ReturnToPool(this);
    }
    
}