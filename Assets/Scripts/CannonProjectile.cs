using System;
using UnityEngine;
using General.Pooling;

public class CannonProjectile : BaseProjectile {

	private Vector3 _cachedTranslation;	//То же самое с направлением
	private Vector3 _defPos;

	public void SetTranslation(Vector3 forward) {
		_cachedTranslation = forward * m_speed;
		_defPos = Tf.position;
	}

	void Update () {
		Tf.Translate(_cachedTranslation * Time.deltaTime);	
		Debug.DrawLine(_defPos, _defPos+_cachedTranslation*100, Color.blue);
		Debug.DrawLine(_defPos, Tf.position, Color.green);
	}

	protected override void ReturnToPool() {
		CannonProjectilePool.Instance.ReturnToPool(this);
	}

	
}