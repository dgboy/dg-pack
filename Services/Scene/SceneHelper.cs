using static DG_Pack.Services.Scene.SceneID;

namespace DG_Pack.Services.Scene {
    public static class SceneHelper {
        public static string GetName(this SceneID scene) => scene switch {
            Game => "Game",
            Home => "Home",
            _ => "Boot",
        };
    }
}