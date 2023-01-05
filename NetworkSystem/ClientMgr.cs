using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;


public class ClientMgr
{
    private Socket tcpSocket;
    private Message message;
    bool isConnected;
    bool isUnfinished;

    public ClientMgr(){
        message = new Message();
    }

    public override void Init()
    {
        tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try{
            tcpSocket.Connect("127.0.0.1", 4567);
            if(tcpSocket.Connected) {
                // Debug.Log("Master Server Connected.");
                isConnected = true;
            }
            StartReceive();
        }catch{
            // Debug.LogWarning(e);
            
        }
        InitUDP();
        
    }
    private void OnDestroy() {
        message = null;
        CloseSocket();
    }
    private void CloseSocket(){
        if(tcpSocket.Connected && tcpSocket != null){
            // Debug.Log("关闭Socket");
            tcpSocket.Close();
        }
    }
    private void StartReceive(){
        // Debug.Log("开始接收");
        tcpSocket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
    }
    bool isFirst = true;
    byte[] bigBuffer;
    int progress=0;
    private void ReceiveCallback(IAsyncResult iar){
        try{
            // Debug.Log("接收到消息");
            if(tcpSocket == null || tcpSocket.Connected == false) return;
            //此为单个包的全部大小 包括包头包体
            int bufferSize = tcpSocket.EndReceive(iar);
            if (isFirst)
            {
                message.InitTotalDataSize();
            }
            if (message.TotalDataSize <= 1020)
            {
                if(bufferSize == 0){
                    // Debug.Log("length为0");
                    CloseSocket();
                    return;
                }
                message.ReadBuffer(bufferSize, HandleResponse);
            }else
            {
                // progress = dataSize + 4;
                if (isFirst)
                {
                    isFirst = false;
                    int dataSize = message.TotalDataSize;
                    bigBuffer = new byte[dataSize + 4];
                }
                
                if (bufferSize > 0)
                {
                    // bigBuffer.Concat(message.Buffer).ToArray();
                    Array.Copy(message.Buffer, 0, bigBuffer, progress, bufferSize);
                    progress += bufferSize;
                    if (progress >= message.TotalDataSize + 4)
                    {
                        message.ReadBigBuffer(bigBuffer, HandleResponse);
                        progress = 0;
                        isFirst = true;
                        bigBuffer = null;
                    }
                }
            }
            StartReceive();
        }catch{
            // Debug.LogError(e.Message);
        }
    }
    private void HandleResponse(MainPack mainPack){
        //处理
        networkMediator.HandleResponse(mainPack);
    }
    public void TcpSend(MainPack mainPack){
        tcpSocket.Send(Message.TcpPackData(mainPack));
    }
    //UDP
    private Socket udpSocket;
    private IPEndPoint iPEndPoint;
    private EndPoint endPoint;
    private byte[] buffer = new byte[1024];
    private Thread thread;
    private void InitUDP(){
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6678);
        endPoint = iPEndPoint;
        try{
            udpSocket.Connect(endPoint);
        }catch{
            // Debug.Log(e.Message);
            return;
        }
        Loom.RunAsync(()=>{
            thread = new Thread(ReceiveMessage);
            thread.Start();
        });
        
    }
    private void ReceiveMessage(){
        while(true){
            int length = udpSocket.ReceiveFrom(buffer, ref endPoint);
            MainPack mainPack = (MainPack) MainPack.Descriptor.Parser.ParseFrom(buffer, 0, length);
            // Loom.QueueOnMainThread(()=>{
            //     HandleResponse(mainPack);
            // });
            HandleResponse(mainPack);
        }
    }
    public void UdpSend(MainPack mainPack){
        mainPack.Uid = networkMediator.playerSelfUid;
        byte[] buffer = Message.UdpPackData(mainPack);
        udpSocket.Send(buffer, buffer.Length, SocketFlags.None);
    }
}


