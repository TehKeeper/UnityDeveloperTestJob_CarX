using System;
using UnityEngine;

namespace Logic.Towers {
    /// <summary> Вращает элементы турели </summary>
    [Serializable]
    public class CannonTurret {
        [SerializeField] private Transform _pivot;
        [SerializeField] float _rotationSpeed = 1;
        //Тут можно сразу, конечно вставить ограничение по оси, но это потом как-нибудь
        
        private Quaternion _cachedTargetRotation;
        private Vector3 _targetAngles;
        private Vector3 _currentAngles;
        private Vector3 _resultAngles;

        public void RotateToTarget(Vector3 targetPosition, RotationAxis constrainedAxis) {
            if (!_pivot) {
                Debug.LogError("Pivot is not set for rotator");
                return;
            }

            RotateToDirection((targetPosition - _pivot.position).normalized, constrainedAxis);
        }

        public void RotateToDirection(Vector3 direction, RotationAxis constrainedAxis) {
            _cachedTargetRotation = Quaternion.LookRotation(direction, Vector3.up);

            _targetAngles = _cachedTargetRotation.eulerAngles;
            _currentAngles = _pivot.rotation.eulerAngles;
            _resultAngles = _currentAngles;
            
            
            switch (constrainedAxis) {
                case RotationAxis.X:
                    _resultAngles.x = Mathf.MoveTowardsAngle(_currentAngles.x, _targetAngles.x, _rotationSpeed * Time.deltaTime);
                    break;
                case RotationAxis.Y:
                    _resultAngles.y = Mathf.MoveTowardsAngle(_currentAngles.y, _targetAngles.y, _rotationSpeed * Time.deltaTime);
                    break;
                case RotationAxis.Z:
                    _resultAngles.z = Mathf.MoveTowardsAngle(_currentAngles.z, _targetAngles.z, _rotationSpeed * Time.deltaTime);
                    break;
            }

            _pivot.rotation = Quaternion.Euler(_resultAngles);
        }
    }
}