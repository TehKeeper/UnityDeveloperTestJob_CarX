using Logic.Monsters;
using UnityEngine;

namespace Logic.Projectiles {
    /// <summary> Базовый класс снаряда </summary>
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

        /// <summary> Инициализация в Awake </summary>
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
            if (other.TryGetComponent(out Monster monster)) {
                //как вариант - можно, конечно, использовать тэг. Но в любом случае придётся обращаться к компоненту
                monster.ApplyDamage(m_damage);
                ReturnToPool();
            }
        }

        /// <summary> Процесс возвращения в пул </summary>
        protected abstract void ReturnToPool();
    }
}