using UnityEngine;

namespace DG_Pack.Inputs {
    public class BackInput : MonoBehaviour {
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) 
                Application.Quit();
        }
    }
}