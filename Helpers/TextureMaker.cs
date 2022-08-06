using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace UtilityPack.Helpers {
    public static class TextureMaker {
        public static bool showLogs = true;

        public static Texture2D LoadImage(string path) {
            if (!File.Exists(path)) return null;

            byte[] fileData = File.ReadAllBytes(path);

            var tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); // this will auto-resize the texture dimensions
            tex.name = $"{Path.GetFileName(path)} {tex.width}x{tex.height}";
            return tex;
        }

        /// <summary>
        /// Не создавать RenderTexture и скриншот в одном кадре, иначе получится чёрный квадрат
        /// </summary>
        /// <param name="rtex"></param>
        /// <returns></returns>
        public static Texture2D MakeScreenshot(Texture rtex) {
            if (showLogs) Debug.Log($"[TH] make a screenshot from camera {rtex.width}x{rtex.height}");

            var prevActive = RenderTexture.active;
            RenderTexture.active = (RenderTexture)rtex;

            Texture2D tex = new Texture2D(rtex.width, rtex.height);
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            tex.Apply();

            RenderTexture.active = prevActive;
            return tex;
        }
        public static bool SaveTexture(string file, Texture2D texture) {
            if (!FileReader.CanSave(file)) return false;

            byte[] _bytes = texture.EncodeToPNG();
            File.WriteAllBytes(file, _bytes);
            if (showLogs) Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + file);

            return texture;
        }
        public static Texture2D CacheThumbnail(string file, Texture rtex) {
            Texture2D thumbnail = MakeScreenshot(rtex);
            return SaveTexture(file, thumbnail) ? thumbnail : null;
        }


        public static Texture2D CacheVideoThumbnail(string path, VideoPlayer player) {
            if (File.Exists(path)) return null;
            FileReader.CreateFolder(path);

            Texture2D thumbnail = MakeVideoPreview(player);
            path += (player.source != VideoSource.Url ? player.clip.name : FileReader.GetFileName(player.url)) + ".png";

            byte[] _bytes = thumbnail.EncodeToPNG();
            File.WriteAllBytes(path, _bytes);
            Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + path);

            return thumbnail;
        }

        public static IEnumerator PrepareVideoPlayer(VideoPlayer source, string path, int w, int h, Action<RenderTexture, float> callback = null) {
            //source.sendFrameReadyEvents = true;
            source.url = path;
            source.Prepare();
            yield return new WaitWhile(() => source && !source.isPrepared);
            source.Pause();
            yield return new WaitForEndOfFrame();
            source.targetTexture = MakeRenderTexture(w, h);

            //do {
            //    source.StepForward();
            //    yield return new WaitForEndOfFrame();
            //} while (source.frame < 1);
            Debug.Log($"[preparing] frame: {source.frame}, count: {source.frameCount}, rate: {source.frameRate}");

            callback?.Invoke(source.targetTexture, (float)source.length);
        }
        public static IEnumerator PrepareVideoPlayer(VideoPlayer source, string path, Action<RenderTexture, float> callback = null) {
            source.url = path;
            //source.sendFrameReadyEvents = true;
            source.Prepare();
            yield return new WaitWhile(() => source && !source.isPrepared);
            source.Pause();
            yield return new WaitForEndOfFrame();
            source.targetTexture = MakeRenderTexture(source.texture.width, source.texture.height);

            //do {
            //    source.StepForward();
            //    yield return new WaitForEndOfFrame();
            //} while (source.frame < 1);
            Debug.Log($"[preparing] frame: {source.frame}, count: {source.frameCount}, rate: {source.frameRate}");

            callback?.Invoke(source.targetTexture, (float)source.length);
        }
        public static IEnumerator PrepareVideoPlayer(VideoPlayer source, string path, bool play, Action<RenderTexture> callback) {
            ReleaseRenderTexture(source);
            source.url = path;
            source.Prepare();
            yield return new WaitWhile(() => source && !source.isPrepared);
            if (!source) yield break;

            source.targetTexture = MakeRenderTexture(source.texture.width, source.texture.height);
            callback(source.targetTexture);
            source.Play();

            if (!play) {
                yield return new WaitWhile(() => source && source.frame <= 1);
                if (!source) yield break;
                source.Stop();
            }
        }
        public static RenderTexture MakeRenderTexture(int w, int h, string name = "rtex") {
            RenderTexture rtex = new(w, h, 24, RenderTextureFormat.ARGB32) {
                name = $"{name} {w}x{h}"
            };
            rtex.Create();
            return rtex;
        }
        public static RenderTexture MakeRenderTexture(Vector2 size, string name = "rtex") {
            size = Vector2Int.RoundToInt(size);
            RenderTexture rtex = new((int)size.x, (int)size.y, 24, RenderTextureFormat.ARGB32) {
                name = $"{name} {size.x}x{size.y}"
            };
            rtex.Create();
            return rtex;
        }
        public static void ReleaseRenderTexture(VideoPlayer source) {
            if (source.isPlaying) {
                RenderTexture.active = null;
                source.Stop();
                source.targetTexture.Release();
            }
        }

        public static Texture2D MakeVideoPreview(VideoPlayer source) {
            int w = source.texture.width;
            int h = source.texture.height;
            if (showLogs) Debug.Log(w + ", " + h);

            Texture2D tex = new Texture2D(w, h);
            var prevActive = RenderTexture.active;
            RenderTexture.active = source.targetTexture;

            tex.ReadPixels(new Rect(0, 0, w, h), 0, 0, false);
            tex.Apply();
            RenderTexture.active = prevActive;

            return tex;
        }
    }
}
