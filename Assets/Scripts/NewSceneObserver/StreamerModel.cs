using System;
using Unity.RenderStreaming;
using UnityEngine;

public class StreamerModel : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private RenderStreaming renderStreaming;
    [SerializeField] private SingleConnection singleConnection;
    [SerializeField] private VideoStreamSender videoStreamSender;
    [SerializeField] private VideoStreamReceiver receiveVideoViewer;
    [SerializeField] private AudioStreamReceiver receiveAudioViewer;
    [SerializeField] private AudioStreamSender microphoneStreamer;
    [SerializeField] private AudioSource receiveAudioSource;
    [SerializeField] private InputReceiver inputReceiver;
#pragma warning restore 0649

    private string _connectionId;
    
    public event Action<Texture> OnUpdateReceiveTextureEvent;
    public event Action<Texture> OnUpdateLocalTextureEvent;

    public void Awake()
    {
        _connectionId = "00000";
        
        receiveVideoViewer.OnUpdateReceiveTexture += texture => OnUpdateReceiveTextureEvent?.Invoke(texture);
        videoStreamSender.OnStartedStream += id => receiveVideoViewer.enabled = true;
        videoStreamSender.OnStartedStream += _ => OnUpdateLocalTextureEvent?.Invoke(videoStreamSender.sourceWebCamTexture);
        receiveAudioViewer.targetAudioSource = receiveAudioSource;
        receiveAudioViewer.OnUpdateReceiveAudioSource += source =>
        {
            source.loop = true;
            source.Play();
        };
    }

    void Start()
    {
        if (renderStreaming.runOnAwake)
            return;
        renderStreaming.Run();
    }

    public void CallUp()
    {
        videoStreamSender.enabled = true;
        microphoneStreamer.enabled = true;
        singleConnection.CreateConnection(_connectionId);
    }

    public void HangUp()
    {
        singleConnection.DeleteConnection(_connectionId);
    }
}


