using System;
using UnityEngine;
using General;
using General.Pooling;
using Towers;

public class CannonTower : MonoBehaviour {
    [Header("Tower stats")]
    [SerializeField] private float m_shootInterval = 0.5f;

    [SerializeField] private float m_range = 4f;

    [SerializeField] private Transform m_shootPoint;

    [SerializeField] private float _shotTime;

    [Header("Turret Parts")]
    [SerializeField] private CannonTurret _turret;
    
    
    private bool _initialized;

    private Vector3 _towerPosition;
    private float _rangeSquared;
    private float _projectileSpeed;
    private Vector3 _interception;
    private CannonProjectile _cachedProjectile;
    private Monster _target;


    private void Awake() {
        if (CannonProjectilePool.Instance == null || m_shootPoint == null) {
            Debug.Log("Проверьте пул объектов и/или точку выстрела");
            return;
        }

        _shotTime = m_shootInterval;
        _towerPosition = transform.position;
        _rangeSquared = m_range * m_range;
        _projectileSpeed = CannonProjectilePool.Instance.GetProjectileSpeed();
        _initialized = true;
    }

    void Update() {
        if (!_initialized)
            return;

        if (_target && (_towerPosition - _target.Tf.position).sqrMagnitude < _rangeSquared) {
            _turret.Rotate(_target.Tf.position, RotationAxis.Y);
        }
        
        foreach (Monster monster in ActiveMonstersHorde.Instance.Monsters) {
            if (Vector3.SqrMagnitude(_towerPosition - monster.transform.position) > _rangeSquared)
                continue;

            _target = monster;
            
            break;
        }

        if (_shotTime > 0) {
            _shotTime -= Time.deltaTime;
            return;
        }


        foreach (Monster monster in ActiveMonstersHorde.Instance.Monsters) {
            if (Vector3.SqrMagnitude(_towerPosition - monster.transform.position) > _rangeSquared)
                continue;

            if (PreemptiveCalculator.TryCalculateInterception(m_shootPoint.position, _projectileSpeed,
                    monster.Tf.position, monster.GetVelocity(), out _interception)) {
                _cachedProjectile = CannonProjectilePool.Instance.GetAtPoint(m_shootPoint);
                _cachedProjectile.SetTranslation((_interception - m_shootPoint.position).normalized);
                _shotTime = m_shootInterval;
            }

            break;
        }
    }
}

[Serializable]
public class CannonTurret {
    [SerializeField] private Transform _pivot;
    [SerializeField] float _rotationSpeed = 1;
    

    public void Rotate(Vector3 targetPosition, RotationAxis constrainedAxis) {
        if(!_pivot)
            return;
        
        // Get direction from this object to the target
        Vector3 direction = (targetPosition - _pivot.position).normalized;

        // Create a rotation only looking toward the target
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Extract just the desired axis angle
        Vector3 targetEuler = targetRotation.eulerAngles;
        Vector3 currentEuler = _pivot.rotation.eulerAngles;
        Vector3 newEuler = currentEuler;
        
        
        switch (constrainedAxis)
        {
            case RotationAxis.X:
                newEuler.x = Mathf.MoveTowardsAngle(currentEuler.x, targetEuler.x, _rotationSpeed * Time.deltaTime);
                break;
            case RotationAxis.Y:
                newEuler.y = Mathf.MoveTowardsAngle(currentEuler.y, targetEuler.y, _rotationSpeed * Time.deltaTime);
                break;
            case RotationAxis.Z:
                newEuler.z = Mathf.MoveTowardsAngle(currentEuler.z, targetEuler.z, _rotationSpeed * Time.deltaTime);
                break;
        }

        _pivot.rotation = Quaternion.Euler(newEuler);
    }
}

[Flags]
public enum RotationAxis {
    X,
    Y,
    Z
}

