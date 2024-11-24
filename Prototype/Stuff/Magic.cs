using UnityEngine;

namespace DGPack.Prototype.Stuff {
    public class Magic : MonoBehaviour {
        [SerializeField] private float max;
        private int current;

        public int Max { get => (int)max; set => max = (int)value; }
        public int Current => current;
        public bool IsExhausted => current <= 0;


        public bool CanUse(int amount) => current >= amount;

        public void Fill() => current = Max;
        public void SetHealth(int amount) => current = amount;
        public void UseAll() => current = 0;


        public virtual void Use(int amount) => current = current - amount <= 0 ? 0 : current - amount;
        public void Restore(int amount) => current = current + amount > Max ? Max : current + amount;

        public void Start() => Fill();
    }
}
