using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketProtocol;

public abstract class IRequest : MonoBehaviour
{
    private string _name = "IRequest";
    public string Name{
        get{ return _name;}
        set{ _name = value;}
    }
    protected RequestCode requestCode;
    protected ActionCode actionCode;
    protected RequestMgr requestMgr;
    protected ClientMgr clientMgr;

    protected NetworkMediator networkMediator;
    // protected PanelMediator panelMediator;

    public ActionCode ActionCode{
        get{ return actionCode;}
    }

    public virtual void Awake() {
        // requestMgr = GameLoop.Instance.gameManager.RequestMgr;
        // clientMgr = GameLoop.Instance.gameManager.ClientMgr;
        // networkMediator = GameLoop.Instance.GetMediator<NetworkMediator>();
        // panelMediator = GameLoop.Instance.GetMediator<PanelMediator>();
        requestMgr.AddRequest(this);
    }

    public virtual void OnDestroy() {
        if (requestMgr!=null)
        {
            requestMgr.RemoveRequest(this);
        }
    }

    public MainPack InitRequest(){
        MainPack mainPack = new MainPack();
        // mainPack.Uid = networkMediator.playerSelfUid;
        mainPack.RequestCode = requestCode;
        mainPack.ActionCode = actionCode;
        return mainPack;
    }

    public abstract void OnResponse(MainPack mainPack);

    public void TcpSendRequest(MainPack mainPack){
        clientMgr.TcpSend(mainPack);
    }

    public void UdpSendRequest(MainPack mainPack){
        clientMgr.UdpSend(mainPack);
    }
}
