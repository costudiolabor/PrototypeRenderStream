using System;
using UnityEngine;

public class Entrance : MonoBehaviour, IDisposable
{
    [SerializeField] private int _frameRate = 100; 
    [SerializeField] private Streamer streamer;
    [SerializeField] private ScreenShotHandler screenShotHandler;
    [SerializeField] private GraphicEditor graphicEditor;
    [SerializeField] private Gallery gallery;
    
   // public event Action<char> CharInputEvent;

    private  void Awake()
    {
        Application.targetFrameRate = _frameRate;
        screenShotHandler.Initialize();
        graphicEditor.Initialize();
        gallery.Initialize();
        streamer.Initialize();
        
        SubscribeEvent();
    }

    private void CallUp() {
        
    }
    
    private void StartEditProcess() {
        CloseViews();
        graphicEditor.OnStart();
        graphicEditor.OnStart();
    }

    public void OpenViews() {
        streamer.ViewOpen();
    }
    
    public void CloseViews() {
        streamer.ViewClose();
        gallery.ViewClose();
    }

    private void SaveGallery(Texture texture) {
        gallery.SaveGallery(texture);
    }

    private void SubscribeEvent() {
        streamer.CallUpEvent += CallUp;
        //streamer.HangUpEvent += HangUp;
        streamer.CharInputEvent += graphicEditor.CharInput;
        screenShotHandler.PointerDownEvent += StartEditProcess;
        graphicEditor.CloseViewEvent += CloseViews;
        graphicEditor.OpenViewEvent += OpenViews;
        graphicEditor.SaveScreenShotEvent += SaveGallery;
    }
  
    private void UnsubscribeEvents() {
        streamer.CallUpEvent -= CallUp;
        //streamer.HangUpEvent -= HangUp;
        streamer.CharInputEvent -= graphicEditor.CharInput;
        screenShotHandler.PointerDownEvent -= StartEditProcess;
        graphicEditor.CloseViewEvent -= CloseViews;
        graphicEditor.OpenViewEvent -= OpenViews;
        graphicEditor.SaveScreenShotEvent -= SaveGallery;
    }
    
    public void Dispose(){
        UnsubscribeEvents();
    }
}