using System.Threading.Tasks;
using DG_Pack.Services.Log;
using UnityEngine.SceneManagement;

namespace DG_Pack.Services.Scene {
    public class SceneService : ISceneService {
        public SceneService(ICustomLogger logger) => _logger = logger;

        private readonly ICustomLogger _logger;
        public string Current => SceneManager.GetActiveScene().name;


        public async Task Load(string scene) {
            if (Current == scene) return;

            _logger.LogTransition(this, Current, scene);

            var operation = SceneManager.LoadSceneAsync(scene);

            while (operation is { isDone: false })
                await Task.Yield();
        }
    }
}