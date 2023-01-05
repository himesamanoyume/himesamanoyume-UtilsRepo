using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketProtocol;

public sealed class ExampleRequest : IRequest
{
    public override void Awake()
    {
        Name = "ExampleRequest";
        requestCode = RequestCode.Example;
        actionCode = ActionCode.ExampleRequest;
        base.Awake();
    }

    public override void OnResponse(MainPack mainPack)
    {
        Loom.QueueOnMainThread(()=>{
            switch(mainPack.ReturnCode){
                case ReturnCode.Success:
                    
                break;
                case ReturnCode.Fail:
                    
                break;
                default:
                    
                break;
            
            }
        });
    }

    public void SendRequest(){
        MainPack mainPack = base.InitRequest();

        base.TcpSendRequest(mainPack);
    }

}
