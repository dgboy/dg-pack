using UnityEditor;
using UnityEngine;

namespace DG_Pack.Editor {
    public static class Tools {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs() {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}