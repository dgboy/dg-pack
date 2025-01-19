using System.Collections.Generic;
using UnityEngine;

namespace DG_Pack.Base.Animation {
    public class SpriteSheetAnimation : MonoBehaviour {
        public SpriteRenderer sr;
        public float fps;

        public List<Sprite> sheet;


        private void Update() {
            sr.sprite = sheet[(int)(Time.time * fps) % sheet.Count];
        }
    }
}