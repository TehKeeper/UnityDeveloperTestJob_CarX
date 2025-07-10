using General.Pooling;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Monsters {
    public class Spawner : MonoBehaviour {
        [FormerlySerializedAs("m_interval")] [SerializeField] private float _spawnInterval = 3;
        [FormerlySerializedAs("m_moveTarget")] [SerializeField] private Transform _moveTarget;

        private float _lastSpawn = 0;
        private Transform _transform;

        private Monster _cachedMonster;

        private void Awake() {
            _transform = transform;
        }

        void Update() {
            if (_lastSpawn >= 0) {
                _lastSpawn -= Time.deltaTime;
                return;
            }

            _cachedMonster = MonsterPool.Instance.GetAtPoint(_transform);
            _cachedMonster.SetTargetPosition(_moveTarget.position);

            _lastSpawn = _spawnInterval;
        }
    }
}