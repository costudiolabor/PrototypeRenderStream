using System;
using Unity.RenderStreaming;
using UnityEngine;

public class InputReceiverData : DataChannelBase {
    public event Action<byte[]> OnMessageEvent; 
    
    protected override void OnMessage(byte[] bytes) {
         OnMessageEvent?.Invoke(bytes);
         //Debug.Log(System.Text.Encoding.UTF8.GetString(bytes));
    }
}
