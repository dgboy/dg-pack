using System;
using System.Collections.Generic;

public abstract class SceneConfig {
    //public abstract Dictionary<Type, Interactor> CreateAllInteractors();
    public abstract Dictionary<Type, Interactor> CreateAllInteractors();

    public void Create<T>(Dictionary<Type, Interactor> interactorsMap) where T : Interactor, new() {
        var interactor = new T();
        var type = typeof(T);
        interactorsMap[type] = interactor;
    }
    public void Create<T>(Dictionary<Type, Repository> repositoriersMap) where T : Repository, new() {
        var repository = new T();
        var type = typeof(T);
        repositoriersMap[type] = repository;
    }
}
