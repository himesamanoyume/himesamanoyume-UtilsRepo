using UnityEngine;

public class Test : MonoBehaviour{
    void Start(){
        EventMgr.AddListener<ExampleNotify>(OnExampleNotify);
    }
    
    void OnExampleNotify(ExampleNotify evt) => ExampleNotify();
    void ExampleNotify(){
        Debug.Log("");
    }

    void AFunc(){
        EventMgr.Broadcast(GameEvents.ExampleNotify);
    }
}