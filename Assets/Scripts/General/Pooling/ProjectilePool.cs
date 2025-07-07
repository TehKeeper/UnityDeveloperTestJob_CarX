using UnityEngine;
using UnityEngine.Pool;

namespace General.Pooling {
    public class ProjectilePool : ObjectPoolGeneric<CannonProjectile> {
        public static ProjectilePool Instance;

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