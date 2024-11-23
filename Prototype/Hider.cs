using UnityEngine;

namespace DG_Pack.Prototype {
    public class Hider : MonoBehaviour {
        private void Awake() => gameObject.SetActive(false);
    }
}