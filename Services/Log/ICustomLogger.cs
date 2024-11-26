namespace DGPack.Services.Log {
    public interface ICustomLogger {
        void Log(string msg, object sender, Dye dye = Dye.None);

        void LogWarning(string msg, object sender);
        void LogError(string msg, object sender);

        void LogTransition(object sender, string current, string next);
        void LogCleanup(object sender, string info = "");
        void LogCheat(object sender, string info);
    }
}