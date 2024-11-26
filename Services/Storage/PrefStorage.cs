using DGPack.Services.Serialization;
using UnityEngine;

namespace DGPack.Services.Storage {
    public class PrefStorage : IStorage {
        private ISerializer Serializer { get; }
        public PrefStorage(ISerializer serializer) => Serializer = serializer;


        public bool Has(string key) => 
            PlayerPrefs.HasKey(key);
        public T Load<T>(string key, T asDefault = default) {
            string data = PlayerPrefs.GetString(key);
            return data != "" ? Serializer.Deserialize<T>(data) : asDefault;
        }
        public void Save<T>(string file, T data) {
            PlayerPrefs.SetString(file, Serializer.Serialize(data));
            PlayerPrefs.Save();
        }

        public void Clear() {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}