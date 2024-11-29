using System.Threading.Tasks;

namespace DG_Pack.Services.Scene {
    public interface ISceneService {
        string Current { get; }
        
        Task Load(string scene);
    }
}