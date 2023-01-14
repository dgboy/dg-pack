using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene {
    private InteractorsBase interactorsBase;
    //private InteractorsBase interactorsBase;
    private SceneConfig sceneConfig;

    public Scene(SceneConfig config) {
        this.sceneConfig = config;
        interactorsBase = new InteractorsBase(config);
        //interactorsBase = new InteractorsBase(config);
    }



    private IEnumerator Init() {
        interactorsBase.CreateAll();
        yield return null;

        interactorsBase.SendOnCreateToAll();
        yield return null;

        interactorsBase.InitAll();
        yield return null;

        interactorsBase.SendOnStartToAll();
        yield return null;
    }
}
