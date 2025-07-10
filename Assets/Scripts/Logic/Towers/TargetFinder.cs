using General;
using UnityEngine;

namespace Logic.Towers {
    public class TargetFinder {
        private Vector3 _towerPosition;
        private float _rangeSquared;

        private float _closestRange = float.PositiveInfinity;
        private float _sqrMagnitude;
        private bool _targetLocked;
        public bool TargetLocked => _targetLocked;


        public TargetFinder(Vector3 towerPosition, float rangeSquared) {
            _towerPosition = towerPosition;
            _rangeSquared = rangeSquared;
        }


        public bool FindTarget(ref Monster target) {
            if (_targetLocked)
                return false;

            _sqrMagnitude = 0;
            _closestRange = float.MaxValue;
            _targetLocked = false;
            target = null;
            foreach (Monster monster in ActiveMonstersHorde.Instance.Monsters) {
                if (monster.IsDead)
                    continue;

                _sqrMagnitude = (_towerPosition - monster.transform.position).sqrMagnitude;
                if (_sqrMagnitude > _rangeSquared)
                    continue;


                if (_closestRange > _sqrMagnitude) {
                    target = monster;
                    _targetLocked = true;
                    _closestRange = _sqrMagnitude;
                }
            }

            if (target) {
                _targetLocked = true;
                target.OnTargetDestroyed += delegate { _targetLocked = false; };
                return true;
            }

            return false;
            //Debug.Break();
        }

        public bool IsValidTarget(Monster target, Vector3 interceptionPoint) {
            return _targetLocked && (_towerPosition - interceptionPoint).sqrMagnitude < _rangeSquared &&
                   target != null && !target.IsDead;
        }
    }
}