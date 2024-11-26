using UnityEngine;

namespace DGPack.Prototype {
    public class Hider : MonoBehaviour {
        private void Awake() => gameObject.SetActive(false);
    }
}