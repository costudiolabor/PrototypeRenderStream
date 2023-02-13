using System;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    //[SerializeField] private Streamer streamer = new Streamer();
    [SerializeField] private ScreenShotHandler screenShotHandler;
    [SerializeField] private GraphicEditor graphicEditor;
    [SerializeField] private Gallery gallery;

    private  void Awake() {
        //streamer.Initialize();
        
        screenShotHandler.Initialize();
        graphicEditor.Initialize();
        gallery.Initialize();
        
        screenShotHandler.PointerDownEvent += StartEditProcess;
        graphicEditor.TakeScreenShot += CloseGallery;
        graphicEditor.SaveScreenShotEvent += SaveGallery;
    }

    public void CloseGallery() {
        gallery.ViewClose();
    }
    
    private void StartEditProcess() {
        graphicEditor.OnStart();
    }

    private void SaveGallery(Texture2D texture) {
        gallery.SaveGallery(texture);
    }
    
}