using System.Collections.Generic;
using UnityEngine;

public class GallaryView : AnimatedView
{
    [SerializeField] private RectTransform parentScreenShot;
    [SerializeField] private Preview preview;
  
    public void OpenPreview(Texture texture) {
        preview.Open();
        preview.SetRawImage(texture);
    }

    public RectTransform GetParent() {
        return parentScreenShot;
    }
}