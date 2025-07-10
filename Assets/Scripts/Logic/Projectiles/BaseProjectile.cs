using Logic.Monsters;
using UnityEngine;

namespace Logic.Projectiles {
    public abstract class BaseProjectile : MonoBehaviour {
        [SerializeField] protected float m_speed = 0.2f;
        [SerializeField] protected int m_damage = 10;

        private const float SELF_DESTRUCT = 5;
        private float _selfDestructTimer = SELF_DESTRUCT;

        /// <summary> gameObject </summary>
        /// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
        public GameObject Go { get; private set; }
    
        /// <summary> transform </summary>
        /// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
        public Transform Tf { get; private set; }

        public float Speed => m_speed;

        private void Awake() {
            Tf = transform; //Кэшируем трансформ, так как каждое обращение - это GetComponent
            Go = gameObject;
            Initialize();
        }

        protected virtual void Initialize() {
        }

        private void LateUpdate() {
            if (_selfDestructTimer > 0) {
                _selfDestructTimer -= Time.deltaTime;
                return;
            }

            _selfDestructTimer = SELF_DESTRUCT;
            ReturnToPool();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<Monster>(out Monster monster)) {
                monster.ApplyDamage(m_damage);
                ReturnToPool();
            }
        }

        protected abstract void ReturnToPool();
    }
}