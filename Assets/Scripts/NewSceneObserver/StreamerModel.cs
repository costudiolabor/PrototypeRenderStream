using System;
using Unity.RenderStreaming;
using Unity.RenderStreaming.Signaling;
using Unity.WebRTC;
using UnityEngine;
using static UnityEngine.Debug;

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

    [SerializeField] private string _connectionId;
    private Vector2Int _screenSize;
    private InputKeyBoard _inputKeyBoard;
    public event Action<Texture> OnUpdateReceiveTextureEvent, OnUpdateLocalTextureEvent;
    public event Action<char> CharInputEvent;
    
    public void Awake()
    {
        SetVideoStreamSize();
        
        _inputKeyBoard = new InputKeyBoard();
        _inputKeyBoard.Initialize(inputReceiver);
        _inputKeyBoard.CharInputEvent += context => CharInputEvent?.Invoke(context);
        
       // _connectionId = "00000";
        
        receiveVideoViewer.OnUpdateReceiveTexture += texture => OnUpdateReceiveTextureEvent?.Invoke(texture);
        
        // videoStreamSender.OnStartedStream += id => { receiveVideoViewer.enabled = true;
        //     //OnUpdateLocalTextureEvent?.Invoke(videoStreamSender.sourceWebCamTexture);
        // };

        receiveVideoViewer.OnStartedStream += id => { inputReceiver.OnStartedChannel += OnStartedChannel;};
        receiveAudioViewer.targetAudioSource = receiveAudioSource;
        
        receiveAudioViewer.OnUpdateReceiveAudioSource += source => {
            source.loop = true;
            source.Play();
        };
    }

    private void SetVideoStreamSize() {
        var scaleResolution = videoStreamSender.scaleResolutionDown;
        _screenSize = new Vector2Int((int)(Screen.width / scaleResolution), (int)(Screen.height / scaleResolution));
        videoStreamSender.width = (uint)_screenSize.x;
        videoStreamSender.height = (uint)_screenSize.y;
    }
    
    void Start() {
        if (renderStreaming.runOnAwake)
            return;
        renderStreaming.Run();
        //inputReceiver.OnStartedChannel += OnStartedChannel;
    }

    private void OnStartedChannel(string connectionId) {
        Rect rect = new Rect(0, 0, _screenSize.x, _screenSize.y);
        inputReceiver.SetInputRange(new Vector2Int(_screenSize.x, _screenSize.y), rect);
        inputReceiver.SetEnableInputPositionCorrection(true);
    }
    
    public void CallUp() {
        videoStreamSender.enabled = true;
        microphoneStreamer.enabled = true;
        singleConnection.CreateConnection(_connectionId);
    }

    public void HangUp() {
        singleConnection.DeleteConnection(_connectionId);
    }
}


