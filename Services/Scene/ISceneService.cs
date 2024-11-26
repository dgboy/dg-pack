using System.Threading.Tasks;

namespace DGPack.Services.Scene {
    public interface ISceneService {
        string Current { get; }
        
        Task Load(string scene);
    }
}