using System.Collections;
using System.Collections.Generic;
using System;
using SocketProtocol;
using Google.Protobuf;
using UnityEngine;
using System.Linq;

public class Message
{
    private byte[] buffer = new byte[1024];
    private int startIndex;

    public byte[] Buffer
    {
        get { return buffer; }
    }

    public int StartIndex
    {
        get { return startIndex; }
    }

    public int Remsize
    {
        get { return buffer.Length - startIndex; }
    }

    public int InitTotalDataSize(){
        totalDataSize = BitConverter.ToInt32(buffer, 0);
        return totalDataSize;
    }

    public int TotalDataSize{
        get { return totalDataSize; }
    }
    int totalDataSize;


    public void ReadBuffer(int bufferSize, Action<MainPack> handleResponse)
    {
        startIndex += bufferSize;
        
        while (true)
        {
            //大小不如以存放包头 说明不完整
            if (startIndex <= 4) return;
            //获取了包体的完整长度 不包括加上包头的长度
            int dataSize = TotalDataSize;
            
            if (startIndex >= dataSize + 4)//
            {
                MainPack mainPack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, dataSize);
                handleResponse(mainPack);
                startIndex -= (dataSize + 4);
            }
            else
            {
                break;
            }
        }

    }

    public void ReadBigBuffer(byte[] bigBuffer, Action<MainPack> handleResponse){
        
        MainPack mainPack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(bigBuffer, 4, TotalDataSize);
        handleResponse(mainPack);
    }

    public static byte[] TcpPackData(MainPack mainPack)
    {
        byte[] data = mainPack.ToByteArray();
        byte[] head = BitConverter.GetBytes(data.Length);
        return head.Concat(data).ToArray();
    }

    public static byte[] UdpPackData(MainPack mainPack){
        return mainPack.ToByteArray();
    }

}
