using UnityEngine;

namespace DG_Pack.Inputs {
    public class ExitInputHandler : MonoBehaviour {
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) 
                Application.Quit();
        }
    }
}