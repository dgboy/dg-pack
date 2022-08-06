using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace UtilityPack.Helpers {
    public static class FileReader {
        public static bool showLogs = true;

        public static string GetDirectoryListingRegexForUrl(string url) {
            // TODO: upd regex pattern
            if (url.Contains("http://95.79.104.203:8102/static/images")) {
                return "<a href=\"image*.*\">(?<name>.*)</a>";
            }
            if (url.Equals("http://95.79.104.203:8102/static/videos/demo")) {
                return "<a href=\"demo*.*\">(?<name>.*)</a>";
            }
            if (url.Contains("http://95.79.104.203:8102/static/videos")) {
                return "<a href=\"video*.*\">(?<name>.*)</a>";
            }
            throw new NotSupportedException();
        }
        public static List<string> GetFiles(string url) {
            List<string> files = new List<string>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    string html = reader.ReadToEnd();

                    Regex regex = new Regex(GetDirectoryListingRegexForUrl(url));
                    MatchCollection matches = regex.Matches(html);

                    if (matches.Count > 0) {
                        Debug.Log($"url: {url}, count: {matches.Count}");
                        foreach (Match match in matches) {
                            if (match.Success) {
                                //Debug.Log(match.Groups["name"]);
                                files.Add(url + "/" + match.Groups["name"]);
                            }
                        }
                        return files;
                    }
                }
            }

            return files;
        }

        // FILES
        public static string GetFileName(string path, bool withExtention = false) {
            return withExtention ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);
        }
        /// <summary>
        /// Создаёт директорию, если её ещё не существует
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolder(string path) {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
                Debug.Log($"[FileReader] folder was created: {path}");
            }
        }
        public static void DeleteFile(string path) {
            if (File.Exists(path)) {
                File.Delete(path);
                Debug.Log($"[FileReader] file was deleted: {path}");
            }
        }
        public static bool CanSave(string path) {
            if (File.Exists(path)) return false;
            string folder = Path.GetDirectoryName(path);
            CreateFolder(folder);
            return true;
        }
        public static void CopyFile(string from, string to) {
            if (!File.Exists(from)) return;

            if (CanSave(to)) {
                File.Copy(from, to, true);
            }
        }
        public static long GetFileCount(string path) {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();

            long count = 0;
            foreach (FileInfo file in files) {
                if (file.Extension.Contains("mp4")) count++;
            }
            return count;
        }
        public static List<(string name, DateTime date)> GetFileInfos(string path, string type = ".mp4") {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            var names = new List<(string name, DateTime date)>();

            foreach (FileInfo file in files) {
                if (file.Extension.Contains(type)) names.Add((file.Name, file.CreationTime));
            }
            return names;
        }


        // MEDIA
        public static IEnumerator LoadImage(string url, string path, Action<Texture2D> callback) {
            if (File.Exists(path)) {
                Debug.Log("File Exist: " + path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(File.ReadAllBytes(path));
                callback(tex);
            } else {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.Log(www.error);
                    yield break;
                } else {
                    Texture2D tex = DownloadHandlerTexture.GetContent(www);
                    byte[] _bytes = tex.EncodeToPNG();
                    File.WriteAllBytes(path, _bytes);
                    Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + path);
                    callback(tex);
                }
            }
        }
        public static IEnumerator LoadVideo(string url, string path, Action callback) {
            if (File.Exists(path)) {
                Debug.Log("File Exist: " + path);
                callback();
            } else {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.Log(www.error);
                    yield break;
                } else {
                    try {
                        File.WriteAllBytes(path, www.downloadHandler.data);
                        callback();
                    } catch (IOException e) {
                        Debug.Log(e);
                        callback();
                    }
                }
            }
        }
        public static IEnumerator GetImage(string url, string path, Action<byte[]> callback) {
            if (File.Exists(path)) {
                Debug.Log("File Exist: " + path);
                callback(File.ReadAllBytes(path));
            } else {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.Log(www.error);
                    yield break;
                } else {
                    Texture2D tex = DownloadHandlerTexture.GetContent(www);
                    Debug.Log("Image was saved as: " + path);
                    callback(tex.EncodeToPNG());
                }
            }
        }
        public static IEnumerator DownloadVideo(string url, string path, Action<byte[]> callback) {
            if (File.Exists(path)) {
                Debug.Log("File Exist: " + path);
                callback(File.ReadAllBytes(path));
            } else {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.Log(www.error);
                    callback(null);
                } else {
                    callback(www.downloadHandler.data);
                }
            }
        }
        public static void GetVideo(string path, Action callback) {
            if (File.Exists(path)) {
                Debug.Log("File Exist: " + path);
                callback();
            }

            callback();
        }


        public static Texture2D GetImage(string path) {
            if (File.Exists(path)) {
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(File.ReadAllBytes(path));
                return texture;
            } else {
                Debug.Log("File Not Exist: " + path);
                return null;
            }
        }
        public static bool IsFileImage(string path) {
            string type = Path.GetExtension(path);
            //Debug.Log("filetype: " + filetype);
            return type == ".png" || type == ".jpg" || type == ".jpeg";
        }

        // JSON
        public static void SaveJson(string path, string data) {
            string folder = Path.GetDirectoryName(path);
            CreateFolder(folder);
            File.WriteAllText(path, data);
        }
        public static string LoadJson(string path) {
            if (File.Exists(path)) {
                return File.ReadAllText(path);
            }
            return null;
        }


        // CACHE
        //public static void Save(string folder, string filename, string type = ".txt") {
        //    var path = Path.Combine(Application.persistentDataPath, folder, filename + type);
        //}
        //public static void Save(string folder, string file) {
        //    var path = Path.Combine(Application.persistentDataPath, folder, file);
        //}
    }
}