using System.Threading.Tasks;
using DGPack.Services.Log;
using UnityEngine.SceneManagement;

namespace DGPack.Services.Scene {
    public class SceneService : ISceneService {
        public SceneService(ICustomLogger logger) => _logger = logger;

        private readonly ICustomLogger _logger;
        public string Current => SceneManager.GetActiveScene().name;


        public async Task Load(string scene) => await DoLoad(scene);


        private async Task DoLoad(string next) {
            if (Current == next) return;
            _logger.LogTransition(this, Current, next);

            var operation = SceneManager.LoadSceneAsync(next);
            while (!operation.isDone)
                await Task.Yield();
        }
    }
}