using UnityEngine;
using UnityEngine.Pool;

namespace General.Pooling {
    public class CannonProjectilePool : ObjectPoolGeneric<CannonProjectile> {
        public static CannonProjectilePool Instance;

        [SerializeField] private PrefabObjectMaker<CannonProjectile> _prefabMaker;

        protected override IObjectMaker<CannonProjectile> ObjectMaker => _prefabMaker;

        protected override void Intialize() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        protected override void EnableItem(CannonProjectile item) {
            item.gameObject.SetActive(true);
        }

        protected override void DisableItem(CannonProjectile item) {
            item.gameObject.SetActive(false);
        }
    }
}