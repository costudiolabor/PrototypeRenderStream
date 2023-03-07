using System;
using UnityEngine;
using Unity.RenderStreaming;

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
    
    [SerializeField] private string connectionId;
    //[SerializeField] private int dialTime;
    //[SerializeField] private InputSender inputSender;
    
    private Vector2Int _screenSize;
    private InputKeyBoard _inputKeyBoard;
    private Coroutine _refCallExpert;

    public event Action<Texture> OnUpdateReceiveTextureEvent, OnUpdateLocalTextureEvent;
    public event Action<char> CharInputEvent;
    public event Action<string>  OnStoppedInputReceiverEvent, OnStartInputReceiverEvent;
    
    public void Awake()
    {
        SetVideoStreamSize();
        
        _inputKeyBoard = new InputKeyBoard();
        _inputKeyBoard.Initialize(inputReceiver);
        _inputKeyBoard.CharInputEvent += context => CharInputEvent?.Invoke(context);
        
        receiveVideoViewer.OnUpdateReceiveTexture += texture => OnUpdateReceiveTextureEvent?.Invoke(texture);
        //receiveVideoViewer.OnStartedStream += id => { inputReceiver.OnStartedChannel += OnStartedChannel;};
        receiveAudioViewer.targetAudioSource = receiveAudioSource;

        receiveAudioViewer.OnUpdateReceiveAudioSource += source => {
            source.loop = true;
            source.Play();
        };
    }
    
    public void SetConnectId(string id)
    {
        connectionId = id;
    }
    
    void Start() {
        if (renderStreaming.runOnAwake)
            return;
        renderStreaming.Run();
        inputReceiver.OnStartedChannel += OnStartedChannel;
    }

    private void SetVideoStreamSize() {
        var scaleResolution = videoStreamSender.scaleResolutionDown;
        _screenSize = new Vector2Int((int)(Screen.width / scaleResolution), (int)(Screen.height / scaleResolution));
        videoStreamSender.width = (uint)_screenSize.x;
        videoStreamSender.height = (uint)_screenSize.y;
    }
    
    private void OnStartedChannel(string channelId) {
        Rect rect = new Rect(0, 0, _screenSize.x, _screenSize.y);
        inputReceiver.SetInputRange(new Vector2Int(_screenSize.x, _screenSize.y), rect);
        inputReceiver.SetEnableInputPositionCorrection(true);
    }
    
    public void CallUp() {
        videoStreamSender.enabled = true;
        microphoneStreamer.enabled = true;
        singleConnection.CreateConnection(connectionId);
        inputReceiver.OnStartedChannel += OnStartedInputReceiver;
        inputReceiver.OnStoppedChannel += OnStoppedInputReceiver;
        
    }
   
    public void HangUp() {
        singleConnection.DeleteConnection(connectionId);
        inputReceiver.OnStartedChannel -= OnStartedInputReceiver;
        inputReceiver.OnStoppedChannel -= OnStoppedInputReceiver;
    }

    private void OnStartedInputReceiver(string id) {
        OnStartInputReceiverEvent?.Invoke(id);
    }
    
    private void OnStoppedInputReceiver(string id) {
        OnStoppedInputReceiverEvent?.Invoke(id);
    }
}


