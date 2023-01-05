using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using SocketProtocol;

public class RequestMgr
{
    public RequestMgr(){
        Name = "RequestMgr";
    }
    public override void Init(){
        // 在主循环添加对应的Request 如
        // GameLoop.Instance.gameObject.AddComponent<AcceptFriendRequestRequest>();
    }

    private Dictionary<ActionCode, IRequest> requestDict = new Dictionary<ActionCode, IRequest>();
    public void AddRequest(IRequest request){
        requestDict.Add(request.ActionCode, request);
    }

    public void RemoveRequest(IRequest request){
        requestDict.Remove(request.ActionCode);
    }
    
    public void HandleResponse(MainPack mainPack){
        // Debug.Log("处理响应："+mainPack.ActionCode);
        if(requestDict.TryGetValue(mainPack.ActionCode, out IRequest request)){
            request.OnResponse(mainPack);
        }else{
            Debug.LogWarning(mainPack.ActionCode+" 找不到对应的处理.");
        }
    }
    public IRequest GetRequest(ActionCode actionCode){
        if(requestDict.TryGetValue(actionCode, out IRequest request)){
            return request; 
        }else{
            Debug.LogWarning(actionCode +" 找不到对应的Request.");
            return null;
        }
    }
}



