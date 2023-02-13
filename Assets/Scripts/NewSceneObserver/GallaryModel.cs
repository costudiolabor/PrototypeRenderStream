using System;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GallaryModel
{
    [SerializeField] private ScreenShotImage prefabScreenShotImage;

    private List<ScreenShotImage> _screenShotImages = new List<ScreenShotImage>(); 
    private RectTransform _parentScreenShot;

    public event Action<Texture2D> ClickImageEvent; 

    public void Initialize(RectTransform parentScreenShot) {
        _parentScreenShot = parentScreenShot;
    }
    
    public void AddScreenShotImage(Texture2D texture2D) {
        ScreenShotImage screenShotImage = Object.Instantiate(prefabScreenShotImage, _parentScreenShot);
        screenShotImage.SetTexture(texture2D);
        screenShotImage.ClickImageEvent += ClickImageEvent;
        _screenShotImages.Add(screenShotImage);
    }

    public bool ThereScreenshots()
    {
        return _screenShotImages.Count > 0;
    }

}
