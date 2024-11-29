using System.Collections;
using DG_Pack.Inputs;
using UnityEngine;

namespace DG_Pack.Prototype {
    public class PlayerMouse : MonoBehaviour {
        public static bool blockInput = false;

        public float speed = 3.0f;
        //private new Rigidbody2D rigidbody;
        //private Vector3 Movement;

        public Transform player;
        public IMoveableMap map;

        public Transform area;

        //private PlayerInput playerInput;
        private Vector3 destination;

        //private void Awake() {
        //    playerInput = new PlayerInput();
        //}
        //public void OnEnable() {
        //    playerInput.Enable();
        //}
        //public void OnDisable() {
        //    playerInput.Disable();
        //}

        public void Start() {
            destination = transform.position;
            //playerInput.Player.LeftClick.performed += _ => MouseClick();
        }

        public IEnumerator Check() {
            yield return new WaitForSeconds(3f);
            //Debug.Log(player.TempMovement);
        }

        public void MouseClick() {
            if (blockInput) return; // move global

            //Vector2 mousePosition = playerInput.Player.MousePosition.ReadValue<Vector2>();
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 gridPosition = map.GetCoordinates(mousePosition);

            if (map.IsPassablePath(gridPosition)) {
                destination = mousePosition;
                var magnitude = -(transform.position - destination).magnitude;
                player.transform.position = (transform.position - destination) / magnitude * speed;
            }
        }

        public void LateUpdate() {
            if (Vector3.Distance(transform.position, destination) > 0.1f) {
                //transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                //player.rigidbody.velocity = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                //player.rigidbody.AddForce(Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime));
                //player.Motion(player.TempMovement);
            } else {
                //player.TempMovement = Vector2.zero;
            }
        }
    }
}