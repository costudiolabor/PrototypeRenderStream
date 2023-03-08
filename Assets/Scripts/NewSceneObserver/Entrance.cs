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

    private  void Awake() {
        Application.targetFrameRate = frameRate;

       // screenShotHandler.Initialize();
        graphicEditor.Initialize();
        gallery.Initialize();
        streamer.Initialize();
        menuMode.Initialize();
        HandlerMessageModel handlerMessageModel = streamer.GetHandlerMessageModel();
        handlerMessage.Initialize(handlerMessageModel);
        
       SubscribeEvent();
    }
    
    private void StartEditProcess() {
        CloseViews();
        graphicEditor.OnStart();
    }

    private void OpenViews() {
        streamer.ViewOpen();
        menuMode.ViewOpen();
        handlerMessage.ViewOpen();
    }

    private void CloseViews() {
        streamer.ViewClose();
        gallery.ViewClose();
        menuMode.ViewClose();
        handlerMessage.ViewClose();

    }

    private void SaveGallery(Texture texture) {
        gallery.SaveGallery(texture);
    }

    private void SubscribeEvent() {
        streamer.CharInputEvent += graphicEditor.CharInput;
        //screenShotHandler.PointerDownEvent += StartEditProcess;
        menuMode.ScreenShotEvent += StartEditProcess;
        graphicEditor.CloseViewEvent += CloseViews;
        graphicEditor.OpenViewEvent += OpenViews;
        graphicEditor.SaveScreenShotEvent += SaveGallery;
    }
  
    private void UnsubscribeEvents() {
        streamer.CharInputEvent -= graphicEditor.CharInput;
        // screenShotHandler.PointerDownEvent -= StartEditProcess;
        menuMode.ScreenShotEvent -= StartEditProcess;
        graphicEditor.CloseViewEvent -= CloseViews;
        graphicEditor.OpenViewEvent -= OpenViews;
        graphicEditor.SaveScreenShotEvent -= SaveGallery;
    }
    
    public void Dispose(){
        UnsubscribeEvents();
    }
}