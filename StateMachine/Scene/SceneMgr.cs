using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneMgr : IManager
{
    private SceneInfo sceneInfo;
    private bool isBegin = false;
    public SceneMediator sceneMediator;

    public override void Init(){
        // Debug.Log("SceneMgr Init start");
        sceneMediator = GameLoop.Instance.GetMediator<SceneMediator>();
    }
    
    public void SetScene(SceneInfo sceneInfo, string sceneName){
        isBegin = false;
        LoadScene(sceneName);
        if(this.sceneInfo!=null){
            this.sceneInfo.OnEnd();
        }
        this.sceneInfo = sceneInfo;
    }
    public void SetScene(SceneInfo sceneInfo){
        // Debug.Log("SetScene");
        isBegin = false;
        if(this.sceneInfo!=null){
            this.sceneInfo.OnEnd();
        }
        this.sceneInfo = sceneInfo;
    }
    public void SceneUpdate(){
        if(sceneInfo != null && isBegin == false){
            // Debug.Log("SceneUpdate");
            
            sceneInfo.OnBegin();
            isBegin = true;
        }
        
        if(sceneInfo != null){
            sceneInfo.OnUpdate();
        }
    }
        
    private void LoadScene(string sceneName){
        if(sceneName == null || sceneName.Length == 0){
            return;
        }
        sceneMediator.LoadScene(sceneName);
        // AssetBundle.UnloadAllAssetBundles(false);
    }
    public string SceneLog(){
        return sceneInfo.Name;
    }
}



