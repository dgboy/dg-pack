using System.Threading.Tasks;
using DG_Pack.Services.Log;
using UnityEngine.SceneManagement;

namespace DG_Pack.Services.Scene {
    public class SceneService : ISceneService {
        public SceneService(ILoadingCurtain loadingCurtain) {
            LoadingCurtain = loadingCurtain;
        }

        private ILoadingCurtain LoadingCurtain { get; }

        public string Current => SceneManager.GetActiveScene().name;


        public async Task Load(string scene) {
            if (Current == scene) return;

            DLogger.LogTransition(this, Current, scene);

            LoadingCurtain.Show();
            await Task.Delay(2000);

            var operation = SceneManager.LoadSceneAsync(scene);

            while (operation is { isDone: false })
                await Task.Yield();

            LoadingCurtain.Hide();
        }
    }
}