using UnityEngine;

namespace DGPack.Services.Log {
    public class DyedLogger : ICustomLogger {
        public void Log(string msg, object sender, Dye dye = Dye.None) =>
            Debug.Log($"<color={dye.GetValue()}>[{sender.GetType().Name}] {msg}</color>");

        public void LogWarning(string msg, object sender) =>
            Debug.LogWarning($"[{sender.GetType().Name}] {msg}");

        public void LogError(string msg, object sender) =>
            Debug.LogError($"[{sender.Class()}] {msg}");


        public void Log(object sender, string msg) =>
            Debug.Log($"<color={Dye.Magenta.GetValue()}>[{sender.GetType().Name}]</color> {msg}");
        public void LogTransition(object sender, string current, string next) {
            string prevStamp = current ?? "[ENTRY]";
            Debug.Log($"[{sender.Class()}] transition: {prevStamp} -> {next}".ToColor(Dye.Blue));
        }
        public void LogCleanup(object sender, string info = "") =>
            Debug.Log($"[{sender.Class()}] C.L.E.A.R => ".ToColor(Dye.White) + info);
        public void LogCheat(object sender, string info) => 
            Debug.Log($"[{sender.Class()}] CHEAT: ".ToColor(Dye.Black) + info);
    }
}