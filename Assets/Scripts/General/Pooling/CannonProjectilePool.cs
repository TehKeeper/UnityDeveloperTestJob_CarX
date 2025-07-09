using UnityEngine;

namespace General.Pooling {
    public class CannonProjectilePool : ProjectilePoolBase<CannonProjectile> {
        public static CannonProjectilePool Instance;

        [SerializeField] private PrefabObjectMaker<CannonProjectile> _prefabMaker;

        protected override IObjectMaker<CannonProjectile> ObjectMaker => _prefabMaker;

        protected override void Intialize() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            
        }
        
        public override float GetProjectileSpeed() => _prefabMaker.Prefab.Speed;
    }
}