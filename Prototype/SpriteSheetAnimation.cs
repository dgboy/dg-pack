using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Prototype {
    public class SpriteSheetAnimation : MonoBehaviour {
        public SpriteRenderer sr;

        public float fps = 6;
        public List<Sprite> sheet;


        private void OnValidate() {
            sr = GetComponent<SpriteRenderer>();

            if (sheet.Count > 0)
                sr.sprite = sheet[0];
        }
        private void Update() {
            sr.sprite = sheet[(int)(Time.time * fps) % sheet.Count];
        }
    }
}