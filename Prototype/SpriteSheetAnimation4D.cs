using System.Collections.Generic;
using DG_Pack.Services.Log;
using UnityEngine;

namespace DG_Pack.Prototype {
    public class SpriteSheetAnimation4D : MonoBehaviour {
        public SpriteRenderer sr;

        [Range(0, 30)] public int fps = 10;

        public bool playing;
        public Direction4 direction;

        public List<Sprite> down;
        public List<Sprite> left;
        public List<Sprite> right;
        public List<Sprite> up;


        private void OnValidate() {
            var sheet = GetActiveSheet();

            if (sheet.Count == 0)
                return;

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = sheet[0];
        }

        private void Update() {
            var sheet = GetActiveSheet();

            if (sheet.Count == 0) {
                DLogger.Log(this, $"No sprite sheet for actor <color=red>[{transform.parent.name}]</color>.");
                enabled = false;
                return;
            }

            sr.sprite = sheet[playing ? (int)(Time.time * fps) % sheet.Count : 0];
        }

        private List<Sprite> GetActiveSheet() => direction switch {
            Direction4.Down => down,
            Direction4.Left => left,
            Direction4.Right => right,
            Direction4.Up => up,
            _ => down,
        };
    }
}