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
        /*if (m_target == null) {
            ReturnToPool();
            return;
        }*/

        //_cachedTranslation = m_target.position - Tf.position;
        /*if (_cachedTranslation.sqrMagnitude > _speedSquared) {
            _cachedTranslation = _cachedTranslation.normalized * m_speed;
        }*/

        Tf.Translate(_cachedTranslation * m_speed * Time.deltaTime);
    }


    protected override void ReturnToPool() {
        GuidedProjectilePool.Instance.ReturnToPool(this);
    }

    public void CalculateVector(Vector3 barrelPosition, Vector3 targetPosition, Vector3 targetVelocity) {
        _interceptPoint =
            PreemptiveCalculator.CalculateInterceptPoint(barrelPosition, m_speed, targetPosition, targetVelocity);
        _cachedTranslation = (_interceptPoint - Tf.position).normalized;
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0.2f, 0.9f, 0.5f, 0.5f);
        Gizmos.DrawSphere(_interceptPoint, 0.5f);
        Debug.DrawLine(Tf.position, _interceptPoint, new Color(0.2f, 0.5f, 0.9f, 0.5f));

            Gizmos.color = new Color(0.2f, 0.9f, 1.0f, 0.5f);
            Gizmos.DrawSphere(Tf.position, 0.2f);
        
    }
}