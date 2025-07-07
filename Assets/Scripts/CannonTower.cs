using UnityEngine;
using General;
using General.Pooling;

public class CannonTower : MonoBehaviour {
	
	[Header("Tower stats")]
	
	[SerializeField] private float m_shootInterval = 0.5f;
	[SerializeField] private float m_range = 4f;
	
	[SerializeField] private Transform m_shootPoint;
	
	private float m_ShotTime = -0.5f;
	private bool m_initialized;

	private Vector3 m_towerPosition;
	private float m_rangeSquared;
	

	private void Awake() {
		if (ProjectilePool.Instance == null || m_shootPoint == null) {
			Debug.Log("Проверьте пул объектов и/или точку выстрела");
			return;
		}

		m_ShotTime = m_shootInterval;
		m_towerPosition = transform.position;
		m_rangeSquared = m_range * m_range;
		m_initialized = true;
	}

	void Update () {
		if (m_initialized)
			return;

		if (m_ShotTime > 0) {
			m_ShotTime -= Time.deltaTime;
			return;
		}
		

		foreach (Monster monster in ActiveMonstersHorde.Instance.m_monsters) {
			if (Vector3.SqrMagnitude (m_towerPosition - monster.transform.position) > m_rangeSquared)
				continue;

			// shot
			ProjectilePool.Instance.GetAtPoint(m_shootPoint);
			m_ShotTime = m_shootInterval;
		}

	}
}
