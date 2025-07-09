using System;
using System.Collections.Generic;
using UnityEngine;
using General.Pooling;
using Towers;

public class CannonProjectile : BaseProjectile {
    private Vector3 _cachedTranslation;

    private Rigidbody _rb;

    private List<Vector3> _track = new();

    public void SetTranslation(Vector3 forward) {
        _cachedTranslation = forward;
    }

    protected override void Initialize() {
        _rb = Go.GetComponent<Rigidbody>();
        _track.Clear();
    }

    public void Launch(Vector3 velocity) {
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    void Update() {
        _track.Add(Tf.position);
        if (_track.Count > 1)
            for (int i = 0; i < _track.Count - 1; i++) {
                Debug.DrawLine(_track[i], _track[i + 1], Color.green);
            }
        //Tf.position += _cachedTranslation * m_speed * Time.deltaTime;

        return;
        Tf.Translate(_cachedTranslation * m_speed * Time.deltaTime, Space.World);
    }

    protected override void ReturnToPool() {
        _track.Clear();
        _rb.Sleep();
        CannonProjectilePool.Instance.ReturnToPool(this);
    }
}