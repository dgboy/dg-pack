using UnityEngine;

namespace DG_Pack.Prototype {
    public class ApplicationExit : MonoBehaviour {
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}