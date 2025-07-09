using System;
using UnityEngine;
using General.Pooling;

public class CannonProjectile : BaseProjectile {

	private Vector3 _cachedTranslation;	//То же самое с направлением
	private Vector3 _defPos;

	public void SetTranslation(Vector3 forward) {
		_cachedTranslation = forward ;
		_defPos = Tf.position;
	}

	void Update () {
		//Tf.position += _cachedTranslation * m_speed * Time.deltaTime;
		Tf.Translate(_cachedTranslation * m_speed * Time.deltaTime, Space.World);	
		Debug.DrawLine(_defPos, _defPos+_cachedTranslation*100, Color.blue);
		Debug.DrawLine(_defPos, Tf.position, Color.green);
	}

	protected override void ReturnToPool() {
		CannonProjectilePool.Instance.ReturnToPool(this);
	}

	
}