using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DGPack.Prototype {
    public class MoveToClickedTile : MonoBehaviour {
        private Vector2 input;

        public Grid map;
        public Tilemap fogOfWar;

        public float speed = 5.0f;
        public Vector2 direction;
        public Vector2 target;

        private Vector2 CellSize => map.cellSize; //new(map.cellSize.x, 1.08f);
        private bool Moving { get; set; }

        private void Start() {
            UpdateFogOfWar();
        }
        private void FixedUpdate() {
            if (Input.GetMouseButtonUp(0)) {
                var cell = (Vector2Int)map.WorldToCell(UnityEngine.Camera.main!.ScreenToWorldPoint(Input.mousePosition));
                var pos = new Vector2(cell.x + (cell.y % 2 != 0 ? 0.5f : 0f), cell.y * 0.75f);
                target = pos * CellSize;
                Debug.Log($"cell={cell} | pos={pos}");
            }


            if (Moving) {
                Move();
                UpdateFogOfWar();

                if (Vector2.Distance(transform.position, target) == 0f)
                    Moving = false;
            } else {
                // direction = ToDirection(input);
                // if (input.x == 0)
                //     return;
                //
                // direction = ToDirection(input);
                // target = (Vector2)transform.position + direction;
                Moving = true;
            }
        }

        public void Move() {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            // transform.position += direction;
        }
        private Vector2 ToDirection(Vector2 value) {
            int y = Math.Sign(value.y);
            return new Vector2(Math.Sign(value.x) * (y == 0 ? 1f : 0.5f), y * 0.5f) * CellSize;
        }

        public int vision = 2;

        void UpdateFogOfWar() {
            var currentPlayerTile = fogOfWar.WorldToCell(transform.position);

            for (int x = -vision; x <= vision; x++) {
                for (int y = -vision; y <= vision; y++)
                    fogOfWar.SetTile(currentPlayerTile + new Vector3Int(x, y, 0), null);
            }
        }
    }
}