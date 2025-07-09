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
    private bool _initialized;

    private Vector3 _towerPosition;
    private float _rangeSquared;
    private float _projectileSpeed;
    private Vector3 _interception;
    private CannonProjectile _cachedProjectile;


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