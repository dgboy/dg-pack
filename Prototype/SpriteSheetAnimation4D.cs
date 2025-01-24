using System.Collections.Generic;
using DG_Pack.Services.Log;
using UnityEngine;

namespace DG_Pack.Prototype {
    public class SpriteSheetAnimation4D : MonoBehaviour {
        public SpriteRenderer sr;

        [Range(0, 30)] public int fps = 10;

        public Vector2 direction;
        public List<Sprite> down;
        public List<Sprite> left;
        public List<Sprite> right;
        public List<Sprite> up;


        private void OnValidate() {
            sr ??= GetComponent<SpriteRenderer>();
            sr.sprite = GetActiveSheet()[0];
        }

        private void Update() {
            var sheet = GetActiveSheet();

            if (sheet.Count == 0) {
                DLogger.Log(this, $"No sprite sheet for actor <color=red>[{transform.parent.name}]</color>.");
                enabled = false;
                return;
            }

            sr.sprite = sheet[(int)(Time.time * fps) % sheet.Count];
        }


        private List<Sprite> GetActiveSheet() {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                if (direction.x < 0) return left;
                if (direction.x > 0) return right;
            } else {
                if (direction.y < 0) return down;
                if (direction.y > 0) return up;
            }


            return down;
        }


        // private List<Sprite> GetActiveSheet() => direction.x switch {
        //     < 0 => left,
        //     > 0 => right,
        //     _ => down,
        // };
        // private List<Sprite> GetActiveSheet() => direction switch {
        //     Direction4.Down => down,
        //     Direction4.Left => left,
        //     Direction4.Right => right,
        //     Direction4.Up => up,
        //     _ => down,
        // };
    }

    // public enum Direction4 {
    //     Down,
    //     Left,
    //     Right,
    //     Up,
    // }
}