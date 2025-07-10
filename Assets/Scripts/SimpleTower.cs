using General;
using General.Pooling;
using UnityEngine;

public class SimpleTower : MonoBehaviour {
	[SerializeField] private Transform m_barrel;
	
	public float m_shootInterval = 0.5f;
	public float m_range = 4f;
	public GameObject m_projectilePrefab;

	private float m_lastShotTime = -0.5f;
	
	void Update () {
		if (m_projectilePrefab == null)
			return;

		foreach (var monster in FindObjectsOfType<Monster>()) {
			if (Vector3.Distance (transform.position, monster.transform.position) > m_range)
				continue;

			if (m_lastShotTime + m_shootInterval > Time.time)
				continue;

			// shot
			GuidedProjectile projectileBeh = GuidedProjectilePool.Instance.GetAtPoint(m_barrel);
			projectileBeh.m_target = monster.Tf;

			m_lastShotTime = Time.time;
		}
	
	}
}
