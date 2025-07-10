using System.Collections.Generic;
using Logic.Monsters;
using UnityEngine;

namespace General {
    /// <summary> Класс для отслеживания активных монстров </summary>
    /// <info> Чтобы не обращаться к FindComponentsOfType </info>
    public class ActiveMonstersHorde : MonoBehaviour {
        public static ActiveMonstersHorde Instance;
        public List<Monster> Monsters;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;

            Monsters = new List<Monster>();
        }

        public void TryAdd(Monster monster) {
            if (!Monsters.Contains(monster)) {
                Monsters.Add(monster);
            }
        }

        public void TryRemove(Monster monster) {
            if (Monsters.Contains(monster)) {
                Monsters.Remove(monster);
            }
        }
    }
}