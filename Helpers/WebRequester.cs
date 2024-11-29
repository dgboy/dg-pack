using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace DG_Pack.Helpers {
    public static class WebRequester {
        public static IEnumerator Get(string url, Action<string> response) {
            using var request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            //Debug.Log($"URL: {url} -> {request.result}");

            switch (request.result) {
                case UnityWebRequest.Result.Success:
                    response(request.downloadHandler.text);
                    break;
                default:
                    Debug.LogError("Error: " + request.error);
                    response(null);
                    break;
            }
        }
        public static IEnumerator Get(string url, Action<Texture2D> response) {
            using var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            //Debug.Log($"URL: {url} -> {request.result}");

            switch (request.result) {
                case UnityWebRequest.Result.Success:
                    Texture2D tex = DownloadHandlerTexture.GetContent(request);
                    tex.name = $"texture {tex.width}x{tex.height}";
                    response(tex);
                    break;
                default:
                    Debug.LogError("Error: " + request.error);
                    response(null);
                    break;
            }
        }

        // WIP - freezes Editor
        public static async Task<string> GetAsync(string url) {
            using var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone) await Task.Yield();

            Debug.Log($"URL: {url} -> {request.result}");

            switch (request.result) {
                case UnityWebRequest.Result.Success:
                    return request.downloadHandler.text;
                default:
                    Debug.LogError("Error: " + request.error);
                    return null;
            }
        }

        // WIP - freezes Editor
        public static async Task<Texture2D> GetTextureAsync(string url) {
            Debug.Log($"URL: {url}");
            using var request = UnityWebRequestTexture.GetTexture(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone) {
                Debug.Log($"async progress: {operation.progress}");
                await Task.Yield();
            }

            Debug.Log($"URL: {url} -> {request.result}");

            switch (request.result) {
                case UnityWebRequest.Result.Success:
                    Texture2D tex = DownloadHandlerTexture.GetContent(request);
                    tex.name = $"texture {tex.width}x{tex.height}";
                    return tex;
                default:
                    Debug.LogError("Error: " + request.error);
                    return null;
            }
        }
    }
}