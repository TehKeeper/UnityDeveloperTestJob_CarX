using General.Pooling;
using Logic.Projectiles;
using UnityEngine;

namespace Logic.Towers {
	public class SimpleTower : BaseTower<GuidedProjectile> {
		private static readonly Vector3 V3Zero = Vector3.zero;
		protected override Vector3 Interception => Radar.TargetLocked ? Target.Tf.position : V3Zero;
		protected override ProjectilePoolBase<GuidedProjectile> Pool => GuidedProjectilePool.Instance;

		protected override void FireProjectile() {
			if (_shotTime + _shootInterval > Time.time)
				return;
		
			GuidedProjectilePool.Instance.GetAtPoint(_shootPoint).SetTarget(Target);

			_shotTime = Time.time;
		}

		protected override void Initialize() { }
	}
}
