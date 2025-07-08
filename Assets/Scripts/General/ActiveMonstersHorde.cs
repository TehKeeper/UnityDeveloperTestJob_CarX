using System.Collections.Generic;
using UnityEngine;

namespace General {
    public class ActiveMonstersHorde : MonoBehaviour {
        public static ActiveMonstersHorde Instance;
        public List<Monster> Monsters { get; private set; }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Monsters = new List<Monster>();
        }

        public void Add(Monster monster) {
            Monsters.Add(monster);
        }

        public void TryRemove(Monster monster) {
            if (Monsters.Contains(monster)) {
                Monsters.Remove(monster);
                Debug.Log("Monster Removed");
            }
        }
    }
}