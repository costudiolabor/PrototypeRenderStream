using System;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GallaryModel {
    [SerializeField] private ScreenShotImage prefabScreenShotImage;

    private List<ScreenShotImage> _screenShotImages = new List<ScreenShotImage>(); 
    private RectTransform _parentScreenShotImage;
    private RectTransform _transformGallery;

    public event Action<Texture> ClickImageEvent; 
    public event Action DestroyImageEvent;

    public void Initialize(RectTransform parentScreenShot) {
        _parentScreenShotImage = parentScreenShot;
    }

    public void SetTransformGallery(RectTransform gallery) {
        _transformGallery = gallery;
    }
    
    public void AddScreenShotImage(Texture texture2D) {
        ScreenShotImage screenShotImage = Object.Instantiate(prefabScreenShotImage, _parentScreenShotImage);
        screenShotImage.SetGallery(_transformGallery);
        screenShotImage.SetTexture(texture2D);
        screenShotImage.ClickImageEvent += ClickImageEvent;
        screenShotImage.DestroyImageEvent += DestroyScreenShotImage;
        _screenShotImages.Add(screenShotImage);
    }

    private void DestroyScreenShotImage(ScreenShotImage screenShotImage) {
        Object.Destroy(screenShotImage.gameObject);
        _screenShotImages.Remove(screenShotImage);
        DestroyImageEvent?.Invoke();
    }

    public bool ThereScreenshots() {
        return _screenShotImages.Count > 0;
    }

}
