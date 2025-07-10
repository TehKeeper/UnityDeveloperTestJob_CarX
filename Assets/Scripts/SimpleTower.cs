using General.Pooling;
using Logic.Towers;
using UnityEngine;

public class SimpleTower : BaseTower {
	protected override Vector3 Interception => Target.Tf.position;

	protected override void FireProjectile() {
		if (_shotTime + m_shootInterval > Time.time)
			return;
		
		GuidedProjectilePool.Instance.GetAtPoint(m_shootPoint).m_target = Target.Tf;

		_shotTime = Time.time;
	}

	protected override bool InitializationCheck() {
		if (GuidedProjectilePool.Instance == null || m_shootPoint == null) {
			Debug.Log("Проверьте пул объектов и/или точку выстрела");
			enabled = false;
			return false;
		}
		
		return true;
	}
}
