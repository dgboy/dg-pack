namespace DG_Pack.Services.Storage {
    public interface IStorage {
        bool Has(string key);
        void Save<T>(string file, T data);
        T Load<T>(string key, T asDefault = default);

        void Clear();
    }
}