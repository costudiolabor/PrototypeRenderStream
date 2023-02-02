using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.RenderStreaming.Samples
{
    public class ControllerRenderStream : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private RenderStreaming renderStreaming;
        [SerializeField] private AudioSource receiveAudioSource;
        [SerializeField] private VideoStreamSender videoStreamSender;
        [SerializeField] private VideoStreamReceiver receiveVideoViewer;
        [SerializeField] private AudioStreamSender microphoneStreamer;
        [SerializeField] private AudioStreamReceiver receiveAudioViewer;
        [SerializeField] private SingleConnection singleConnection;
        [SerializeField] private InputReceiver inputReceiver;
        [SerializeField] private ViewRenderStream _viewRenderStream;
#pragma warning restore 0649
        
        private string _connectionId;
        private RenderStreamingSettings _settings;
        
        public void Init(ViewRenderStream viewRenderStream)
        {
            _connectionId = "00000";

            _viewRenderStream = viewRenderStream;
            _viewRenderStream.StartButtonEvent += StartButton;
            _viewRenderStream.SetUpButton += SetUp;
            _viewRenderStream.HangUpButton += HangUp;
            _settings = SampleManager.Instance.Settings;
            videoStreamSender.OnStartedStream += id => receiveVideoViewer.enabled = true;
            if (_settings != null)
            {
                if (videoStreamSender.source != VideoStreamSource.Texture)
                {
                    videoStreamSender.width = (uint)_settings.StreamSize.x;
                    videoStreamSender.height = (uint)_settings.StreamSize.y;
                }

                videoStreamSender.SetCodec(_settings.SenderVideoCodec);
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
            renderStreaming.Run(signaling: _settings?.Signaling);
            inputReceiver.OnStartedChannel += OnStartedChannel;
        }

        void OnUpdateReceiveTexture(Texture texture)
        {
            _viewRenderStream.SetRemoteVideoImage(texture);
            //remoteVideoImage.texture = texture;
        }

        private void OnStartedChannel(string connectionId)
        {
            inputReceiver.SetInputRange(new Vector2Int((int)videoStreamSender.width, (int)videoStreamSender.height));
            inputReceiver.SetEnableInputPositionCorrection(true);
        }

        private void StartButton()
        {
            videoStreamSender.enabled = true;
            microphoneStreamer.enabled = true;
        }

        private void SetUp()
        {
            if (_settings != null)
            {
                receiveVideoViewer.SetCodec(_settings.ReceiverVideoCodec);
            }
            singleConnection.CreateConnection(_connectionId);
        }

        private void HangUp()
        {
            singleConnection.DeleteConnection(_connectionId);
            _viewRenderStream.SetRemoteVideoImage(null);
            _viewRenderStream.SetLocalVideoImage(null);
            
            //remoteVideoImage.texture = null;
            //localVideoImage.texture = null;
        }
    }
}