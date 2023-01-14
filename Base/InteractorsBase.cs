using System;
using System.Collections.Generic;

public class InteractorsBase {
    private Dictionary<Type, Interactor> interactorsMap;
    private SceneConfig sceneConfig;

    public InteractorsBase(SceneConfig config) {
        this.sceneConfig = config;
        //interactorsMap = new Dictionary<Type, Interactor>();
    }


    public void CreateAll() {
        interactorsMap = sceneConfig.CreateAllInteractors();
    }

    public void InitAll() {
        var interactors = interactorsMap.Values;
        foreach (var interactor in interactors) {
            interactor.Init();
        }
    }

    public void SendOnStartToAll() {
        var interactors = interactorsMap.Values;
        foreach (var interactor in interactors) {
            interactor.OnStart();
        }
    }
    public void SendOnCreateToAll() {
        var interactors = interactorsMap.Values;
        foreach (var interactor in interactors) {
            interactor.OnCreate();
        }
    }

    public T GetInteractor<T>() where T : Interactor {
        var type = typeof(T);
        return (T)interactorsMap[type];
    }
}
