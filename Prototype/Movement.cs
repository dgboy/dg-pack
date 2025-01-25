using UnityEngine;

namespace DG_Pack.Prototype {
    public class Movement : MonoBehaviour {
        public Direction4 direction;
        public Vector2 delta;
        public bool active;


        public void Apply(Vector2 value) {
            delta = value;
            active = delta != Vector2.zero;

            if (active)
                direction = delta.From();
        }
    }
}