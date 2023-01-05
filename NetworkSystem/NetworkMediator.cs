using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMediator
{
    public Client clientMgr;
    public Request requestMgr;

    private void Start() {
        Init();
    }
    public override void Init()
    {
        //clientMgr = GameLoop.Instance.gameManager.ClientMgr;
        //requestMgr = GameLoop.Instance.gameManager.RequestMgr;
    }
    public void TcpSend(MainPack mainPack){
        clientMgr.TcpSend(mainPack);
    }
    public void UdpSend(MainPack mainPack){
        clientMgr.UdpSend(mainPack);
    }
    public void AddRequest(IRequest request)
    {
        requestMgr.AddRequest(request);
    }
    public void RemoveRequest(IRequest request)
    {
        requestMgr.RemoveRequest(request);
    }
    public void HandleResponse(MainPack mainPack){
        requestMgr.HandleResponse(mainPack);
    }
    
}

