using UnityEngine;
using General;
using General.Pooling;

public class CannonTower : MonoBehaviour {
	
	[Header("Tower stats")]
	
	[SerializeField] private float m_shootInterval = 0.5f;
	[SerializeField] private float m_range = 4f;
	
	[SerializeField] private Transform m_shootPoint;
	
	private float _shotTime;
	private bool _initialized;

	private Vector3 _towerPosition;
	private float _rangeSquared;
	

	private void Awake() {
		if (ProjectilePool.Instance == null || m_shootPoint == null) {
			Debug.Log("Проверьте пул объектов и/или точку выстрела");
			return;
		}

		_shotTime = m_shootInterval;
		_towerPosition = transform.position;
		_rangeSquared = m_range * m_range;
		_initialized = true;
	}

	void Update () {
		if (_initialized)
			return;

		if (_shotTime > 0) {
			_shotTime -= Time.deltaTime;
			return;
		}
		

		foreach (Monster monster in ActiveMonstersHorde.Instance.Monsters) {
			if (Vector3.SqrMagnitude (_towerPosition - monster.transform.position) > _rangeSquared)
				continue;

			// shot
			ProjectilePool.Instance.GetAtPoint(m_shootPoint);
			_shotTime = m_shootInterval;
		}

	}
}
