using UnityEngine;
using System.Collections;
using General.Pooling;

public class CannonProjectile : MonoBehaviour {
	[SerializeField] private float m_speed = 0.2f;
	[SerializeField] private int m_damage = 10;

	private Transform _transform; //Кэшируем трансформ, так как каждое обращение - это GetComponent
	private Vector3 _cachedTranslation;	//То же самое с направлением

	private void Awake() {
		_transform = transform;
		_cachedTranslation = _transform.forward * m_speed;
	}

	void Update () {
		_transform.Translate(_cachedTranslation * Time.deltaTime);	
	}

	void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent<Monster>(out Monster monster)) {
			monster.ApplyDamage(m_damage);
			ProjectilePool.Instance.ReturnToPool(this);
		}
		
	}
}
