using UnityEngine;

namespace DG_Pack.UI.Canvas.Modals.Loading {
    public class SimpleLoading : MonoBehaviour {
        private RectTransform rectComponent;
        public float rotateSpeed = 200f;

        void Start () {
            rectComponent = GetComponent<RectTransform>();
        }
	
        void Update () {
            rectComponent.Rotate(0f, 0f, -(rotateSpeed * Time.deltaTime));
        }
    }
}
