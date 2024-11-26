using UnityEngine;

namespace DGPack.Prototype {
    public class ApplicationExit : MonoBehaviour {
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}