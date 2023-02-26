using System;
using Unity.RenderStreaming;

public class InputReceiverData : DataChannelBase {
    public event Action<byte[]> OnMessageEvent; 
    
    protected override void OnMessage(byte[] bytes) {
         OnMessageEvent?.Invoke(bytes);
         //Debug.Log(System.Text.Encoding.UTF8.GetString(bytes));
    }
}
