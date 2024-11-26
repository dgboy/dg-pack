using UnityEngine;

namespace DGPack.Services.Serialization {
    public class UnityJsonSerializer : ISerializer {
        public string Serialize(object data) =>
            JsonUtility.ToJson(data);

        public T Deserialize<T>(string data) =>
            JsonUtility.FromJson<T>(data);
    }
}