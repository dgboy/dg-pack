using System.Collections.Generic;
using UnityEngine;

namespace AdvancedCursor {
    public class CursorManager : MonoBehaviour {
        public static CursorManager self;

        public enum Type {
            Arrow,
            Move,
            Grab,
            Use,
            Attack,
        }

        [System.Serializable]
        public class Animation {
            public Type type;
            public List<Texture2D> textures;
            public float frameRate;
            public Vector2 offset;
        }

        [SerializeField] private List<Animation> cursorAnimations;
        private Animation cursorAnimation;

        private int currentFrame;
        //private float frameRate = 0.1f;
        private float frameTimer;

        private void Awake() {
            self = this;
        }
        private void Start() {
            SetDefaultCursor();
        }
        private void Update() {
            frameTimer -= Time.deltaTime;
            if (frameTimer <= 0f) {
                frameTimer += cursorAnimation.frameRate;
                currentFrame = (currentFrame + 1) % cursorAnimation.textures.Count;
                Cursor.SetCursor(cursorAnimation.textures[currentFrame], cursorAnimation.offset, CursorMode.Auto);
            }
        }

        public void SetDefaultCursor() {
            SetCursor(Type.Arrow);
        }
        // public void SetPreviousCursor() {
        //     SetCursor(Type.Arrow);
        // }
        public void SetCursor(Type type) {
            // TODO: if null
            SetActiveAnimation(GetAnimation(type));
        }

        private Animation GetAnimation(Type type) {
            foreach (Animation cursorAnimation in cursorAnimations) {
                if (cursorAnimation.type == type) return cursorAnimation;
            }
            return null;
        }
        private void SetActiveAnimation(Animation animation) {
            this.cursorAnimation = animation;
            Debug.Log(animation.type);
            currentFrame = 0;
            frameTimer = animation.frameRate;
        }
    }
}