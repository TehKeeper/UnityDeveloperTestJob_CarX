using System;
using UnityEngine;
using General;
using General.Pooling;

public class Monster : MonoBehaviour {
    public float m_speed = 2f;
    public int m_maxHP = 30;
    const float m_reachDistance = 0.5477f;

    public int m_hp;

    private float _reachDistanceSquared;

    private Vector3 m_moveTarget;
    private Vector3 _translation;

    /// <summary> gameObject </summary>
    /// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
    public GameObject Go { get; private set; }

    /// <summary> transform </summary>
    /// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
    public Transform Tf { get; private set; }

    private void Awake() {
        Tf = transform;
        Go = gameObject;
        _reachDistanceSquared = m_reachDistance * m_reachDistance;
    }

    void OnEnable() {
        m_hp = m_maxHP;
        ActiveMonstersHorde.Instance.Add(this);
    }

    void Update() {
        /*if (m_moveTarget == null)
            return;*/

        
        if ((Tf.position - m_moveTarget).sqrMagnitude <= _reachDistanceSquared) {
            MonsterPool.Instance.ReturnToPool(this);
            return;
        }

        Tf.Translate(_translation*Time.deltaTime);
    }

    public void ApplyDamage(int mDamage) {
        m_hp -= mDamage;
        if (m_hp <= 0) {
            MonsterPool.Instance.ReturnToPool(this);
        }
    }

    public void SetTargetPosition(Vector3 target) {
        m_moveTarget = target;
        _translation = (m_moveTarget - Tf.position).normalized * m_speed;
    }

    public Vector3 GetVelocity() => _translation * m_speed;

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0.9f, 0.4f, 0.0f, 0.5f);
        Gizmos.DrawSphere(Tf.position, 0.2f);
    }
}