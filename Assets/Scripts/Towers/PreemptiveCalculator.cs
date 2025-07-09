using UnityEngine;

namespace Towers {
    public static class PreemptiveCalculator {
        private static float _a;
        private static float _b;
        private static float _discriminant;
        private static Vector3 _pathToTarget;
        private static float _time1;
        private static float _time2;
        private static float _resultTime;


        public static Vector3 CalculateInterceptPoint(Vector3 barrelPosition, float projectileSpeed, Vector3 targetPos,
            Vector3 targetVelocity) {
            Vector3 toTarget = targetPos - barrelPosition;
            Debug.DrawLine(targetPos, barrelPosition, Color.red);

            _a = targetVelocity.sqrMagnitude - projectileSpeed * projectileSpeed;
            _b = 2f * Vector3.Dot(toTarget, targetVelocity);

            _discriminant = _b * _b - 4f * _a * toTarget.sqrMagnitude;

            if (_discriminant < 0f) {
                Debug.LogError("No solution, aim directly at the current position");
                return targetPos;
            }

            float sqrtDisc = Mathf.Sqrt(_discriminant);
            float t1 = (-_b + sqrtDisc) / (4 * _a);
            float t2 = (-_b - sqrtDisc) / (4 * _a);

            float t = Mathf.Max(t1, t2); 
            if (t < 0f) {
                Debug.LogError("all times are negative, shoot directly");
                return targetPos; 
            }

            Debug.DrawLine(targetPos, barrelPosition, Color.red);
            return targetPos + targetVelocity * t;
        }

        public static bool TryCalculateInterception(Vector3 barrelPosition, float projectileSpeed, Vector3 targetPos,
            Vector3 targetVelocity, out Vector3 interception) {
            
            _pathToTarget = targetPos - barrelPosition;
            
            interception = targetPos;
            _a = targetVelocity.sqrMagnitude - projectileSpeed * projectileSpeed;
            _b = 2f * Vector3.Dot(_pathToTarget, targetVelocity);

            _discriminant = _b * _b - 4f * _a * _pathToTarget.sqrMagnitude;

            if (_discriminant < 0f) {
                return false;
            }

            _discriminant = Mathf.Sqrt(_discriminant);
            _time1 = (-_b + _discriminant) / (4 * _a);
            _time2 = (-_b - _discriminant) / (4 * _a);

            _resultTime = Mathf.Max(_time1, _time2); 
            if (_resultTime < 0f) {
                return false; 
            }

            Debug.DrawLine(targetPos, barrelPosition, Color.red);
            interception = targetPos + targetVelocity * _resultTime;
            Debug.Break();
            return true;
        }
    }
}