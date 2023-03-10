using System;
using UnityEngine;

public class Entrance : MonoBehaviour, IDisposable
{
    [SerializeField] private int frameRate = 100;
    [SerializeField] private Streamer streamer;
    [SerializeField] private HandlerMessage handlerMessage;
    //[SerializeField] private ScreenShotHandler screenShotHandler;
    [SerializeField] private GraphicEditor graphicEditor;
    [SerializeField] private Gallery gallery;
    [SerializeField] private MenuMode menuMode;
    [SerializeField] private Editor3DMarker editor3DMarker;

    private  void Awake() {
        Application.targetFrameRate = frameRate;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

       // screenShotHandler.Initialize();
        graphicEditor.Initialize();
        gallery.Initialize();
        streamer.Initialize();
        HandlerMessageModel handlerMessageModel = streamer.GetHandlerMessageModel();
        handlerMessage.Initialize(handlerMessageModel);
        editor3DMarker.Initialize();
        menuMode.Initialize();
        
       SubscribeEvent();
    }
    
    private void StartEditProcess() {
        CloseViews();
        graphicEditor.OnStart();
    }

    private void SelectMarker3D()
    {
        editor3DMarker.ViewOpen();
    }

    private void OpenViews() {
        streamer.ViewOpen();
        handlerMessage.ViewOpen();
        menuMode.ViewOpen();
    }

    private void CloseViews() {
        streamer.ViewClose();
        gallery.ViewClose();
        handlerMessage.ViewClose();
        editor3DMarker.ViewClose();
        menuMode.ViewClose();
    }

    private void SaveGallery(Texture texture) {
        gallery.SaveGallery(texture);
    }

    private void SubscribeEvent() {
        //streamer.CharInputEvent += graphicEditor.CharInput;
        streamer.CharInputEvent += editor3DMarker.CharInput;
        graphicEditor.CloseViewEvent += CloseViews;
        graphicEditor.OpenViewEvent += OpenViews;
        graphicEditor.SaveScreenShotEvent += SaveGallery;
        menuMode.ScreenShotEvent += StartEditProcess;
        menuMode.SelectMarker3DEvent += SelectMarker3D;
    }
  
    private void UnsubscribeEvents() {
        //streamer.CharInputEvent -= graphicEditor.CharInput;
        streamer.CharInputEvent -= editor3DMarker.CharInput;
        graphicEditor.CloseViewEvent -= CloseViews;
        graphicEditor.OpenViewEvent -= OpenViews;
        graphicEditor.SaveScreenShotEvent -= SaveGallery;
        menuMode.ScreenShotEvent -= StartEditProcess;
    }
    
    public void Dispose(){
        UnsubscribeEvents();
    }
}