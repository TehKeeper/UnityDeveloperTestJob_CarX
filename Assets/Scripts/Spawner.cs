using System;
using UnityEngine;
using System.Collections;
using General.Pooling;

public class Spawner : MonoBehaviour {
	[SerializeField] private float m_interval = 3;
	[SerializeField] private Transform m_moveTarget;

	private float m_lastSpawn = 3;
	private Transform _transform;

	private void Awake() {
		_transform = transform;
		m_lastSpawn = m_interval;
	}

	void Update () {
		if (m_lastSpawn >= 0) {
			m_lastSpawn -= Time.deltaTime;
			return;
		}
		
		Monster monster = MonsterPool.Instance.GetAtPoint(_transform);
			
		monster.m_moveTarget = m_moveTarget;

		m_lastSpawn = m_interval;
		
		
		if (Time.time > m_lastSpawn + m_interval) {
			
		}
	}
}
