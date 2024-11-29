using UnityEngine;

namespace DG_Pack.Services.Log {
    public static class DLogger {
        public static void Log(string msg, object sender, Dye dye = Dye.None) =>
            Debug.Log($"<color={dye.GetValue()}>[{sender.Class()}] {msg}</color>");

        public static void LogWarning(string msg, object sender) =>
            Debug.LogWarning($"[{sender.Class()}] {msg}");

        public static void LogError(string msg, object sender) =>
            Debug.LogError($"[{sender.Class()}] {msg}");


        public static void Log(object sender, string msg) =>
            Debug.Log($"<color={Dye.Magenta.GetValue()}>[{sender.Class()}]</color> {msg}");
        public static void LogTransition(object sender, string current, string next) {
            string prevStamp = current ?? "[ENTRY]";
            Debug.Log($"[{sender.Class()}] transition: {prevStamp} -> {next}".ToColor(Dye.Blue));
        }
        public static void LogCleanup(object sender, string info = "") =>
            Debug.Log($"[{sender.Class()}] C.L.E.A.R => ".ToColor(Dye.White) + info);
        public static void LogCheat(object sender, string info) => 
            Debug.Log($"[{sender.Class()}] CHEAT: ".ToColor(Dye.Black) + info);
    }
}