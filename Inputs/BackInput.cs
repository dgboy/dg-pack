using UnityEngine;

namespace Inputs {
    public class BackInput : MonoBehaviour {
        void Start() {
            // Smart Back (WIP)
            //event Screen.GoTo(next)
            //     save prev_screen
            //     if input back ->
            //         screen.GoTo(prev_screen)
            //event Screen.Show()
            //     save cur_screen
            //     if input back ->
            //         cur_screen.Hide()
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }
}