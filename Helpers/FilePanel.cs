//using SFB;
using System.IO;
using UnityEngine;
//using UnityEditor;

public static class FilePanel /*: EditorWindow */ {


    //[MenuItem("Example/Overwrite Texture")]
    //static void Apply() {
    //    Texture2D texture = Selection.activeObject as Texture2D;
    //    if (texture == null) {
    //        EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
    //        return;
    //    }

    //    string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
    //    if (path.Length != 0) {
    //        var fileContent = File.ReadAllBytes(path);
    //        texture.LoadImage(fileContent);
    //    }
    //}
    public static string[] Select() {
        //Debug.Log($"Opening...");
        //var extensions = new[] {
        //    new ExtensionFilter("Media Files", "png", "jpg", "jpeg"),
        //};

#if PLATFORM_STANDALONE || UNITY_EDITOR
        //return StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
#else
        //if (Application.isEditor) {
        //    StartPreview(StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true));
        //} else {
        //    PickMixedMedias((paths) => {
        //        StartPreview(paths);
        //    });
        //}
        return null;
#endif
        return null;
    }
}

