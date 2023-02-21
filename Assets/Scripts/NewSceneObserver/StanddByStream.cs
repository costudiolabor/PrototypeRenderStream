using System.Collections;
using System.Collections.Generic;
using Unity.RenderStreaming;
using UnityEngine;

public class StanddByStream : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private RenderStreaming renderStreaming;
    [SerializeField] private SingleConnection singleConnection;
    [SerializeField] private InputReceiver inputReceiver;
    [SerializeField] private InputSenderData inputSenderData;
    
#pragma warning restore 0649
}






public class InputData : DataChannelBase
{
    protected override void OnMessage(byte[] bytes)
    {
        base.OnMessage(bytes);
    }
}