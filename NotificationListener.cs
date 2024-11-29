using UnityEngine;
using UnityEngine.Events;

namespace DG_Pack {
    public class NotificationListener : MonoBehaviour {
        [SerializeField] public Notification myNotification;
        public UnityEvent myEvent;

        public void OnEnable() {
            myNotification.RegisterListener(this);
        }

        public void OnDisable() {
            myNotification.DeregisterListener(this);
        }

        public void Raise() {
            myEvent.Invoke();
        }
    }
}
