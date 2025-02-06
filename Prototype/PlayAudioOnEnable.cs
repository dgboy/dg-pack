using UnityEngine;

namespace DG_Pack.Prototype {
    [RequireComponent(typeof(AudioSource))]
    public class PlayAudioOnEnable : MonoBehaviour {
        private void OnEnable() {
            GetComponent<AudioSource>().Play();
        }
    }
}