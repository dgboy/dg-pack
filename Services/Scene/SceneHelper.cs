namespace DGPack.Services.Scene {
    public static class SceneHelper {
        public static string GetName(this SceneID scene) => scene switch {
            SceneID.Game => "Game",
            SceneID.Home => "Home",
            _ => "Boot",
        };
    }
}