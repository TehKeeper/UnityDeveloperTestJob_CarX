namespace General.Pooling {
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