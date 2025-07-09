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


        public static bool TryCalculateInterception(Vector3 barrelPosition, float projectileSpeed, Vector3 targetPos,
            Vector3 targetVelocity, out Vector3 interception, out float time) {
            _pathToTarget = targetPos - barrelPosition;
            time = 0;

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
            time = _resultTime;

            return true;
        }

        
        public static bool CalculateParabolicVelocity(Vector3 start, Vector3 end,  float time, out Vector3 velocity,  bool forceUpwardArc = false)
        {
            velocity = Vector3.zero;


            if (time <= 0f)
                return false;

            Vector3 displacement = end - start;
            Vector3 gravityTerm = 0.5f * Physics.gravity * time * time;

            velocity = (displacement - gravityTerm) / time;

            if (forceUpwardArc && velocity.y < 0f)
            {
                velocity = (displacement + gravityTerm) / time;
            }
            

            return true;
        }
        
    }
}