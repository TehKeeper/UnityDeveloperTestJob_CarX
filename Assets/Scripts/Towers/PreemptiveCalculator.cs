using UnityEngine;

namespace Towers {
    public static class PreemptiveCalculator {
        private static float _a;
        private static float _b;
        private static float _discriminant;


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

            float t = Mathf.Max(t1, t2); // Pick the positive (future) time
            if (t < 0f) {
                Debug.LogError("all times are negative, shoot directly");
                return targetPos; // If all times are negative, shoot directly
            }

            Debug.DrawLine(targetPos, barrelPosition, Color.red);
            Debug.Break();
            return targetPos + targetVelocity * t;
        }
    }
}