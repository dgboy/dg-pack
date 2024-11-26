using System.IO;
using DGPack.Services.Serialization;
using UnityEngine;

namespace DGPack.Services.Storage {
    public class FileStorage : IStorage {
        private ISerializer Serializer { get; }
        public FileStorage(ISerializer serializer) => Serializer = serializer;


        private const string FolderName = "saves";
        private static string DataPath => Application.persistentDataPath;
        private static string FolderPath => Path.Combine(DataPath, FolderName);


        public bool Has(string key) => File.Exists(BuildPath(key));
        public T Load<T>(string key, T asDefault = default) {
            string data = Has(key) ? File.ReadAllText(BuildPath(key)) : "";
            return data != "" ? Serializer.Deserialize<T>(data) : asDefault;
        }
        public void Save<T>(string file, T data) {
            File.WriteAllText(BuildPath(file), Serializer.Serialize(data));
        }

        public void Clear() {
            var dir = new DirectoryInfo(FolderPath);
            dir.Delete();
        }


        private static string BuildPath(string key) {
            // Debug.Log($"directory [{FolderPath}] is exist: {Directory.Exists(FolderPath)}");
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            return Path.Combine(FolderPath, key);
        }
    }
}