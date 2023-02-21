using System;
using UnityEngine;

[Serializable]
public class Gallery : ViewOperator<GallaryView>
{
   [SerializeField] private GallaryModel galleryModel;
   
   public void Initialize(){
      base.CreateView();
      galleryModel.Initialize(view.GetParentScreenShotImage());
      galleryModel.SetTransformGallery(view.GetTransformGallery());
      galleryModel.ClickImageEvent += OpenPreview;
      galleryModel.DestroyImageEvent += CheckGallery;
   }

   public void SaveGallery(Texture texture) {
      galleryModel.AddScreenShotImage(texture);
      CheckGallery();
   }

   private void CheckGallery() {
      if (galleryModel.ThereScreenshots())
         ViewOpen();
      else
         ViewClose();
      ClosePreview();
   }

   public void ViewOpen() {
      view.Open();
   }

   public void ViewClose() {
      view.ForceClose();
   }
   
   private void OpenPreview(Texture texture) {
      view.OpenPreview(texture);
   }

   private void ClosePreview() {
      view.ClosePreview();
   }
}
