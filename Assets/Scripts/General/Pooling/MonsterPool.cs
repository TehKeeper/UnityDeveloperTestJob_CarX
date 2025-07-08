using UnityEngine;

namespace General.Pooling {
    public class MonsterPool : ObjectPoolGeneric<Monster> {
        public static MonsterPool Instance;

        protected override IObjectMaker<Monster> ObjectMaker => _monsterMaker;

        private CapsuleObjectMaker<Monster> _monsterMaker = new ();

        protected override void Intialize() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        protected override void EnableItem(Monster item) {
            item.Go.SetActive(true);
            item.Tf.parent = null;
            ActiveMonstersHorde.Instance.Add(item);
        }

        protected override void DisableItem(Monster item) {
            item.Go.SetActive(false);
            ActiveMonstersHorde.Instance.TryRemove(item);

            item.Tf.position = PhysicalStorage.position;
            item.Tf.parent = PhysicalStorage;
        }
    }
}