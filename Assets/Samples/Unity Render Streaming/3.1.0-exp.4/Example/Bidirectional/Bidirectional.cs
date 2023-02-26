using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.RenderStreaming.Samples
{
    class Bidirectional : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private RenderStreaming renderStreaming;
        [SerializeField] private Button startButton;
        [SerializeField] private Button setUpButton;
        [SerializeField] private Button hangUpButton;
        [SerializeField] private RawImage localVideoImage;
        [SerializeField] private RawImage remoteVideoImage;
        [SerializeField] private AudioSource receiveAudioSource;
        [SerializeField] private VideoStreamSender videoStreamSender;
        [SerializeField] private VideoStreamReceiver receiveVideoViewer;
        [SerializeField] private AudioStreamSender microphoneStreamer;
        [SerializeField] private AudioStreamReceiver receiveAudioViewer;
        [SerializeField] private SingleConnection singleConnection;
       // [SerializeField] private InputReceiver inputReceiver;


#pragma warning restore 0649

        private string connectionId;
        private RenderStreamingSettings settings;

        void Awake()
        {
            startButton.interactable = true;
            setUpButton.interactable = false;
            hangUpButton.interactable = false;
            startButton.onClick.AddListener(() =>
            {
                videoStreamSender.enabled = true;
                startButton.interactable = false;
                microphoneStreamer.enabled = true;
                setUpButton.interactable = true;
            });
            setUpButton.onClick.AddListener(SetUp);
            hangUpButton.onClick.AddListener(HangUp);
            videoStreamSender.OnStartedStream += id => receiveVideoViewer.enabled = true;
            settings = SampleManager.Instance.Settings;


            if (settings != null)
            {
                if (videoStreamSender.source != VideoStreamSource.Texture) 
                {
                    videoStreamSender.width = (uint)settings.StreamSize.x;
                    videoStreamSender.height = (uint)settings.StreamSize.y;
                }
                videoStreamSender.SetCodec(settings.SenderVideoCodec);
            }
            receiveVideoViewer.OnUpdateReceiveTexture += OnUpdateReceiveTexture;

            Microphone.devices.Select(x => new Dropdown.OptionData(x)).ToList();

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
            renderStreaming.Run(signaling: settings?.Signaling);
           // inputReceiver.OnStartedChannel += OnStartedChannel;
        }
        
        void OnUpdateReceiveTexture(Texture texture)
        {
            remoteVideoImage.texture = texture;
        }
        private void OnStartedChannel(string connectionId)
        {
           // inputReceiver.SetInputRange(new Vector2Int((int)videoStreamSender.width, (int)videoStreamSender.height));
           // inputReceiver.SetEnableInputPositionCorrection(true);
        }

        private void SetUp()
        {
            setUpButton.interactable = false;
            hangUpButton.interactable = true;
            if (settings != null)
            {
                receiveVideoViewer.SetCodec(settings.ReceiverVideoCodec);
            }

            connectionId = "00000";
            singleConnection.CreateConnection(connectionId);
        }

        private void HangUp()
        {
            singleConnection.DeleteConnection(connectionId);
            remoteVideoImage.texture = null;
            setUpButton.interactable = true;
            hangUpButton.interactable = false;
            localVideoImage.texture = null;
        }
    }
}