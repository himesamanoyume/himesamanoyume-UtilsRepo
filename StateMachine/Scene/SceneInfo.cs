using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReTouchGunFire.Mediators;

public abstract class SceneInfo
{
    protected SceneMediator sceneMediator;
    public SceneInfo(SceneMediator sceneMediator){
        this.sceneMediator = sceneMediator;
    }

    public abstract void OnBegin();
    public abstract void OnUpdate();
    public abstract void OnEnd();
}
