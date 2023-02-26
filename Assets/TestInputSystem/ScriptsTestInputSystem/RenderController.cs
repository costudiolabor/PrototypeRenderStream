using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming;


static class InputSenderExtension
{
    public static void SetInputRange(this InputSender sender, RawImage image)
    {
        // correct pointer position
        Vector3[] corners = new Vector3[4];
        image.rectTransform.GetWorldCorners(corners);
        Camera camera = image.canvas.worldCamera;
        var corner0 = RectTransformUtility.WorldToScreenPoint(camera, corners[0]);
        var corner2 = RectTransformUtility.WorldToScreenPoint(camera, corners[2]);
        var region = new Rect(
            corner0.x,
            corner0.y,
            corner2.x - corner0.x,
            corner2.y - corner0.y
            );

        var size = new Vector2Int(image.texture.width, image.texture.height);
        sender.SetInputRange(region, size);
    }
}


public class RenderController : MonoBehaviour
{
    [SerializeField] private RenderStreaming renderStreaming;
    [SerializeField] private SingleConnection singleConnection;
    [SerializeField] private VideoStreamSender videoStreamSender;
    [SerializeField] private VideoStreamReceiver videoStreamReceiver;
    [SerializeField] private InputSender inputSender;
    [SerializeField] private InputReceiver inputReceiver;


    [SerializeField] private RawImage remoteImage;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button EndButton;


    [SerializeField] private string connectionId;

    private Vector2Int _screenSize;

    private void Awake()
    {
        SetVideoStreamSize();

        videoStreamReceiver.OnUpdateReceiveTexture += texture =>
        {
            remoteImage.texture = texture;
            SetInputChange();
        };

        inputSender.OnStartedChannel += OnStartedChannelSender;


        StartButton.onClick.AddListener(()=>CallUp());
        EndButton.onClick.AddListener(() => HangUp());
    }

    private void Start()
    {
        if (renderStreaming.runOnAwake)
            return;
        renderStreaming.Run();
        inputReceiver.OnStartedChannel += OnStartedChannel;
    }

    void OnStartedChannelSender(string connectionId)
    {
        SetInputChange();
    }

    void SetInputChange()
    {
        if (inputSender == null || !inputSender.IsConnected || remoteImage.texture == null)
            return;
        inputSender.SetInputRange(remoteImage);
        inputSender.EnableInputPositionCorrection(true);
    }



    private void OnStartedChannel(string channelId)
    {
        Rect rect = new Rect(0, 0, _screenSize.x, _screenSize.y);
        inputReceiver.SetInputRange(new Vector2Int(_screenSize.x, _screenSize.y), rect);
        inputReceiver.SetEnableInputPositionCorrection(true);
    }

    private void SetVideoStreamSize()
    {
        var scaleResolution = videoStreamSender.scaleResolutionDown;
        _screenSize = new Vector2Int((int)(Screen.width / scaleResolution), (int)(Screen.height / scaleResolution));
        videoStreamSender.width = (uint)_screenSize.x;
        videoStreamSender.height = (uint)_screenSize.y;
    }

    public void CallUp()
    {
        videoStreamSender.enabled = true;
        videoStreamReceiver.enabled = true;

        StartButton.interactable = false;
        EndButton.interactable = true;

        singleConnection.CreateConnection(connectionId);
    }

    public void HangUp()
    {
        videoStreamSender.enabled = false;
        videoStreamReceiver.enabled = false;

        StartButton.interactable = true;
        EndButton.interactable = false;

        singleConnection.DeleteConnection(connectionId);
    }


}
