using Logic.Projectiles;

namespace General.Pooling {
    /// <summary> Базовый класс для пулинга снарядов </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ProjectilePoolBase<T> : ObjectPoolGeneric<T> where T: BaseProjectile {
        protected override void EnableItem(T item) {
            item.Go.SetActive(true);
        }
        
        protected override void DisableItem(T item) {
            item.Go.SetActive(false);
        }

        public abstract float GetProjectileSpeed();
    }
}