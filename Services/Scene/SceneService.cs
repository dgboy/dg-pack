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
            LoadingCurtain.Show();
            var operation = SceneManager.LoadSceneAsync(scene);

            if (operation == null) {
                DLogger.LogError($"Сцена {scene} не найдена!", this);
                return;
            }

            operation.allowSceneActivation = false;

            while (LoadingCurtain.Playing) // || operation.progress < 0.9f
                await Task.Yield();

            operation.allowSceneActivation = true;
            DLogger.LogTransition(this, Current, scene);
            LoadingCurtain.Hide();
        }
    }
}