using UnityEngine;
using General.Pooling;

public class CannonProjectile : BaseProjectile {

	private Vector3 _cachedTranslation;	//То же самое с направлением
	
	private void OnEnable() {
		_cachedTranslation = Tf.forward * m_speed;
	}

	void Update () {
		Tf.Translate(_cachedTranslation * Time.deltaTime * m_speed);	
	}

	void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent<Monster>(out Monster monster)) {
			monster.ApplyDamage(m_damage);
		}
	}

	protected override void ReturnToPool() {
		CannonProjectilePool.Instance.ReturnToPool(this);
	}
}


public abstract class BaseProjectile : MonoBehaviour {
	[SerializeField] protected float m_speed = 0.2f;
	[SerializeField] protected int m_damage = 10;
	
	/// <summary> gameObject </summary>
	/// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
	public GameObject Go { get; private set; }
    
	/// <summary> transform </summary>
	/// <info>Сокращено чтобы не пересекалось с наименованием класса</info>
	public Transform Tf { get; private set; }

	private void Awake() {
		Tf = transform; //Кэшируем трансформ, так как каждое обращение - это GetComponent
		Go = gameObject;
	}
	
	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent<Monster>(out Monster monster)) {
			monster.ApplyDamage(m_damage);
			ReturnToPool();
		}
	}

	protected abstract void ReturnToPool();
}