using System;
using UnityEngine;
using System.Collections;
using General;
using General.Pooling;
using UnityEditor;

public class Monster : MonoBehaviour {

	public Transform m_moveTarget;
	public float m_speed = 0.1f;
	public int m_maxHP = 30;
	const float m_reachDistance = 0.3f;

	public int m_hp;
	private Transform _transform;

	private void Awake() {
		_transform = transform;
	}

	void OnEnable() {
		m_hp = m_maxHP;
		ActiveMonstersHorde.Instance.Add(this);
	}

	void Update () {
		if (m_moveTarget == null)
			return;
		
		if (Vector3.Distance (_transform.position, m_moveTarget.position) <= m_reachDistance) {
			Destroy (gameObject);
			return;
		}

		var translation = m_moveTarget.position - _transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		_transform.Translate (translation);
	}

	public void ApplyDamage(int mDamage) {
		m_hp -= mDamage;
		if (m_hp <= 0) {
			MonsterPool.Instance.ReturnToPool(this);

		}
	}
}
