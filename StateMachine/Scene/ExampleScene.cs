using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ExampleScene : SceneInfo
{
    public InitScene(SceneMediator sceneMediator) : base(sceneMediator){
        Name = "InitScene";
    }

    public override void OnBegin()
    {
        // Debug.Log("InitScene Begin");
    }

    public override void OnUpdate()
    {
        // Debug.Log("InitScene Update");
    }

    public override void OnEnd()
    {
        // Debug.Log("InitScene End");
        
    }
}
