using System;
using General;
using General.Pooling;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Monsters {
    public class Monster : MonoBehaviour {
        private const float SPEED = 2f;
        private const int MAX_HP = 30;
        private const float REACH_DISTANCE_SQUARED = 3f;
        public event Action OnTargetDestroyed;

        private int _hp;


        private Vector3 _moveTarget;
        private Vector3 _translation;
        private bool _targetSet;


        public bool IsDead { get; private set; }

        /// <summary> gameObject </summary>
        /// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
        public GameObject Go { get; private set; }

        /// <summary> transform </summary>
        /// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
        public Transform Tf { get; private set; }

        public Vector3 GetVelocity() => _translation * SPEED;

        private void Awake() {
            Tf = transform;
            Go = gameObject;
        }

        void OnEnable() {
            IsDead = false;
            _hp = MAX_HP;
            ActiveMonstersHorde.Instance.TryAdd(this);
        }

        void Update() {
            if (IsDead || !_targetSet)
                return;

            if ((Tf.position - _moveTarget).sqrMagnitude <= REACH_DISTANCE_SQUARED) {
                DisableMonster();
                return;
            }

            Tf.Translate(_translation * Time.deltaTime);
        }

        private void DisableMonster() {
            IsDead = true;
            _targetSet = false;
            MonsterPool.Instance.ReturnToPool(this);
            OnTargetDestroyed?.Invoke();
            OnTargetDestroyed = null;
        }

        public void ApplyDamage(int mDamage) {
            _hp -= mDamage;
            if (_hp <= 0) {
                DisableMonster();
            }
        }

        public void SetTargetPosition(Vector3 target) {
            _moveTarget = target;
            _translation = (_moveTarget - Tf.position).normalized * SPEED;
            _targetSet = true;
        }


        private void OnDestroy() {
            OnTargetDestroyed?.Invoke();
            OnTargetDestroyed = null;
        }
    }
}