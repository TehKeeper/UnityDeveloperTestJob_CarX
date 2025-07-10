using General.Pooling;
using UnityEngine;

namespace Logic.Towers {
	public class SimpleTower : BaseTower {
		private static readonly Vector3 V3Zero = Vector3.zero;
		protected override Vector3 Interception => Radar.TargetLocked ? Target.Tf.position : V3Zero;

		protected override void FireProjectile() {
			if (_shotTime + _shootInterval > Time.time)
				return;
		
			GuidedProjectilePool.Instance.GetAtPoint(_shootPoint).SetTarget(Target.Tf);

			_shotTime = Time.time;
		}

		protected override bool InitializationCheck() {
			if (GuidedProjectilePool.Instance == null || _shootPoint == null) {
				Debug.Log("Проверьте пул объектов и/или точку выстрела");
				enabled = false;
				return false;
			}
		
			return true;
		}
	}
}
