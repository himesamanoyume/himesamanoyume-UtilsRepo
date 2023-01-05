using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneMediator
{
    public SceneMgr sceneMgr;
    
    public SceneMediator(){
        Name = "SceneMediator";
    }
    private void Awake() {
        Init();
    }
    public void Init()
    {
        // sceneMgr = GameLoop.Instance.gameManager.SceneMgr;
    }
    public void SetScene(SceneInfo sceneInfo, string sceneName){
        sceneMgr.SetScene(sceneInfo, sceneName);
    }
    public void SetScene(SceneInfo sceneInfo){
        sceneMgr.SetScene(sceneInfo);
    }
    public void SceneUpdate(){
        sceneMgr.SceneUpdate();
    }
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public string SceneLog(){
        return sceneMgr.SceneLog();
    }

    //转换状态示例
    void SetMainScene(){
        sceneMgr.SetScene(new MainScene(this));
    }
    
}



