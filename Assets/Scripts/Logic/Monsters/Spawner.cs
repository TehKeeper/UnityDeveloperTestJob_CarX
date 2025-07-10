using General.Pooling;
using UnityEngine;

namespace Logic.Monsters {
	public class Spawner : MonoBehaviour {
		[SerializeField] private float m_interval = 3;
		[SerializeField] private Transform m_moveTarget;

		private float m_lastSpawn = 3;
		private Transform _transform;

		private Monster _cachedMonster;

		private void Awake() {
			_transform = transform;
			m_lastSpawn = 0;
		}

		void Update () {
			if (m_lastSpawn >= 0) {
				m_lastSpawn -= Time.deltaTime;
				return;
			}
		
			_cachedMonster = MonsterPool.Instance.GetAtPoint(_transform);
			_cachedMonster.SetTargetPosition(m_moveTarget.position);

			m_lastSpawn = m_interval;
		}
	}
}
