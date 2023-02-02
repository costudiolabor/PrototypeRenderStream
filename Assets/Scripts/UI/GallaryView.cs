using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GallaryView : ViewBase
{
    [SerializeField] private RectTransform parentScreenShot;
    [SerializeField] private List<ScreenShotImage> screenShotImages; // = new List<ScreenShotImage>();
    [SerializeField] private ScreenShotImage prefabScreenShotImage;
    [SerializeField] private Preview preview;

    public void AddScreenShotImage(Texture2D texture2D)
    {
        ScreenShotImage screenShotImage = Instantiate(prefabScreenShotImage, parentScreenShot);
        screenShotImage.SetTexture(texture2D);
        screenShotImage.ClickImageEvent += ClosePreview;
        screenShotImages.Add(screenShotImage);
    }

    private void ClosePreview(Texture2D texture)
    {
        preview.Open();
        preview.SetRawImage(texture);
    } 
    
    public override void Close()
    {
        if (screenShotImages.Count == 0) base.Close();
    }
    
    
    
}