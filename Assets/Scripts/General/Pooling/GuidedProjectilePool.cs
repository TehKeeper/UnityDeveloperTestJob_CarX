using General.Pooling.Makers;
using Logic.Projectiles;
using UnityEngine;

namespace General.Pooling {
    public class GuidedProjectilePool : ProjectilePoolBase<GuidedProjectile> {
        public static GuidedProjectilePool Instance;
        [SerializeField] private PrefabObjectMaker<GuidedProjectile> _prefabMaker;
        protected override IObjectMaker<GuidedProjectile> ObjectMaker => _prefabMaker;
        protected override void Intialize() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            
        }
        
        public override float GetProjectileSpeed() => _prefabMaker.Prefab.Speed;
        
    }
}