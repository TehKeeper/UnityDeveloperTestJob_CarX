using UnityEngine;

namespace General.Pooling {
    public class CannonProjectileLinePool : ProjectilePoolBase<CannonProjectileLine> {
        public static CannonProjectileLinePool Instance;

        [SerializeField] private PrefabObjectMaker<CannonProjectileLine> _prefabMaker;

        protected override IObjectMaker<CannonProjectileLine> ObjectMaker => _prefabMaker;

        protected override void Intialize() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            
        }
        
        public override float GetProjectileSpeed() => _prefabMaker.Prefab.Speed;
    }
}