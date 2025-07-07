using System.Collections.Generic;
using UnityEngine;

namespace General {
    public class ActiveMonstersHorde : MonoBehaviour {
        public static ActiveMonstersHorde Instance;
        public List<Monster> m_monsters = new List<Monster>();

       private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

       public void Add(Monster monster) {
           m_monsters.Add(monster);
       }

       public void Remove(Monster monster) {
           m_monsters.Remove(monster);
       }
    }
}