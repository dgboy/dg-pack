using UnityEngine;

namespace DG_Pack.Base {
    public class Hider : MonoBehaviour {
        private void Awake() => gameObject.SetActive(false);
    }
}