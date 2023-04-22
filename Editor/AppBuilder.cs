using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using static UnityEditor.BuildPipeline;

namespace DG_Pack.Editor {
    public static class AppBuilder {
        // public static void Test() => Debug.Log(BuildFolderPath);

        [MenuItem("Build/🅰️ All")]
        public static void BuildAll() {
            BuildAndroid();
            BuildWebGL();
            BuildWindows();
            // IOS
            // Linux
            // MacOS
        }

        [MenuItem("Build/🤖 Android")]
        public static void BuildAndroid() {
            MakeBuild(BuildTarget.Android, $"{BuildName}.apk");
        }

        [MenuItem("Build/🕸️ WebGL")]
        public static void BuildWebGL() {
            MakeBuild(BuildTarget.WebGL, $"[{BuildTarget.WebGL}] {BuildName}");
        }

        [MenuItem("Build/🪟 Windows")]
        public static void BuildWindows() {
            MakeBuild(
                BuildTarget.StandaloneWindows,
                $"[Windows] {BuildName}/{Application.productName}.exe"
            );
        }


        private static void MakeBuild(BuildTarget target, string targetPath) {
            var report = BuildPlayer(
                new BuildPlayerOptions {
                    target = target,
                    locationPathName = Path.Combine(BuildFolderPath, targetPath),
                    scenes = ScenesInBuild,
                }
            );

            if (report.summary.result != BuildResult.Succeeded) {
                throw new Exception(GetErrorMessage(target));
            }
        }

        private static string[] ScenesInBuild => EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
        private static string BuildName => $"{Application.productName} v{Application.version}";
        private static string ProjectFolder => new DirectoryInfo(Path.Combine(Application.dataPath, "../")).Name;
        private static string BuildFolderPath => Path.Combine(Application.dataPath, "../../_builds", ProjectFolder);
        private static string GetErrorMessage(BuildTarget target) => $"Failed to build {target} package. See log for details.";
    }
}
