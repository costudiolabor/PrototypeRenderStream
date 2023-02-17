using UnityEngine;

public class GallaryView : AnimatedView
{
    [SerializeField] private RectTransform gallery;
    [SerializeField] private RectTransform parentScreenShotImage;
    [SerializeField] private Preview preview;
  
    public void OpenPreview(Texture texture) {
        preview.Open();
        preview.SetRawImage(texture);
    }

    public RectTransform GetParentScreenShotImage() {
        return parentScreenShotImage;
    }

    public RectTransform GetTransformGallery() {
        return gallery;
    }
}