using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DG_Pack.Editor {
    /// <summary>
    /// Editor functionalities from internal SceneHierarchyWindow and SceneHierarchy classes. 
    /// For that we are using reflection.
    /// </summary>
    public static class SceneHierarchyHelper {
        public static void Expand(bool expand) {
            SetExpanded(SceneManager.GetActiveScene(), expand);
        }
        private static void SetExpanded(Scene scene, bool expand) {
            foreach (var window in Resources.FindObjectsOfTypeAll<SearchableEditorWindow>()) {
                if (window.GetType().Name != "SceneHierarchyWindow")
                    continue;

                var method = window.GetType().GetMethod("SetExpandedRecursive",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance, null,
                    new[] {
                        typeof(int), typeof(bool)
                    }, null);

                if (method == null) {
                    Debug.LogError("Could not find method 'UnityEditor.SceneHierarchyWindow.SetExpandedRecursive(int, bool)'.");
                    return;
                }

                var field = scene.GetType().GetField("m_Handle", BindingFlags.NonPublic | BindingFlags.Instance);

                if (field == null) {
                    Debug.LogError("Could not find field 'int UnityEngine.SceneManagement.Scene.m_Handle'.");
                    return;
                }

                object sceneHandle = field.GetValue(scene);
                method.Invoke(window, new[] {
                    sceneHandle, expand
                });
            }
        }


        /// <summary>
        /// Check if the target GameObject is expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static bool IsExpanded(GameObject go) {
            return GetExpandedGameObjects().Contains(go);
        }

        /// <summary>
        /// Get a list of all GameObjects which are expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static List<GameObject> GetExpandedGameObjects() {
            object sceneHierarchy = GetSceneHierarchy();

            MethodInfo methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("GetExpandedGameObjects");

            object result = methodInfo.Invoke(sceneHierarchy, new object[0]);

            return (List<GameObject>)result;
        }

        /// <summary>
        /// Set the target GameObject as expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static void SetExpanded(GameObject go, bool expand) {
            object sceneHierarchy = GetSceneHierarchy();

            var methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("ExpandTreeViewItem", BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo == null) return;
            methodInfo.Invoke(sceneHierarchy, new object[] {
                go.GetInstanceID(), expand
            });
        }

        /// <summary>
        /// Set the target GameObject and all children as expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static void SetExpandedRecursive(GameObject go, bool expand) {
            object sceneHierarchy = GetSceneHierarchy();

            var methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("SetExpandedRecursive", BindingFlags.Public | BindingFlags.Instance);

            methodInfo.Invoke(sceneHierarchy, new object[] {
                go.GetInstanceID(), expand
            });
        }

        private static object GetSceneHierarchy() {
            var window = GetHierarchyWindow();

            object sceneHierarchy = typeof(EditorWindow).Assembly
                .GetType("UnityEditor.SceneHierarchyWindow")
                .GetProperty("sceneHierarchy")
                ?.GetValue(window);

            return sceneHierarchy;
        }

        private static EditorWindow GetHierarchyWindow() {
            // For it to open, so that it the current focused window.
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            return EditorWindow.focusedWindow;
        }
    }
}