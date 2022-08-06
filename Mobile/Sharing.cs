using UnityEngine;

namespace UtilityPack.Mobile {
    public static class Sharing {
        public static void Share(string msg) {
#if UNITY_EDITOR
            Debug.Log(msg);
#elif UNITY_ANDROID || UNITY_IOS
        new NativeShare()
            .SetText(msg)
            //.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
#endif
        }

        public static void Share(string msg, string path) {
#if UNITY_EDITOR
            Debug.Log($"msg: {msg}, path: {path}");
#elif UNITY_ANDROID || UNITY_IOS
        new NativeShare()
            .SetText(msg)
            .AddFile(path)
            .Share();
#endif
        }
    }
}