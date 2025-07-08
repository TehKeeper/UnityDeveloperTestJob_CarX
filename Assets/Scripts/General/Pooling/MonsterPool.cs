namespace General.Pooling {
    public class MonsterPool : ObjectPoolGeneric<Monster> {
        public static MonsterPool Instance;

        protected override void Intialize() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        protected override void EnableItem(Monster item) {
            item.gameObject.SetActive(true);
            ActiveMonstersHorde.Instance.Add(item);
        }

        protected override void DisableItem(Monster item) {
            item.gameObject.SetActive(false);
        }
    }
}