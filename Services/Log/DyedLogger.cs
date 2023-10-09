using UnityEngine;

namespace DG_Pack.Services.Log {
    public class DyedLogger : ICustomLogger {
        public void Log(string msg, object sender, Dye dye = Dye.None) =>
            Debug.Log($"<color={dye.GetValue()}>[{sender.GetType().Name}] {msg}</color>");

        public void LogWarning(string msg, object sender) =>
            Debug.LogWarning($"[{sender.GetType().Name}] {msg}");

        public void LogError(string msg, object sender) =>
            Debug.LogError($"[{sender.ClassName()}] {msg}");


        public void Log(object sender, string msg) =>
            Debug.Log($"<color={Dye.Magenta.GetValue()}>[{sender.GetType().Name}]</color> {msg}");
        public void LogTransition(object sender, object current, object next) {
            string prevStamp = current != null ? current.ClassName() : "[ENTRY]";
            Debug.Log($"[{sender.ClassName()}] transition: {prevStamp} -> {next.ClassName()}".ToColor(Dye.Blue));
        }
        public void LogCleanup(object sender, string info = "") =>
            Debug.Log($"[{sender.ClassName()}] C.L.E.A.R => ".ToColor(Dye.White) + info);
        public void LogCheat(object sender, string info) => 
            Debug.Log($"[{sender.ClassName()}] CHEAT: ".ToColor(Dye.Black) + info);
    }
}