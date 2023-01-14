using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    public class InventoryRepository : Repository {
        public static string KEY = "coins";
        public float currentMagic;

        public int Coins { get; set; } = 0;
        public float Mana { get; set; } = 10;


        public override void Init() {
            Coins = PlayerPrefs.GetInt(KEY);
        }
        public void AddCoins(object sender, int amount) {
            Coins += amount;
            Debug.Log(sender);
        }

        public void SubCoins(object sender, int amount) {
            Coins -= amount;
            Debug.Log(sender);
        }
    }
}