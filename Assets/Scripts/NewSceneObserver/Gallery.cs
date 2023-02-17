using System;
using UnityEngine;

[Serializable]
public class Gallery : ViewOperator<GallaryView>
{
   [SerializeField] private GallaryModel galleryModel;
   
   public void Initialize(){
      base.CreateView();
      galleryModel.Initialize(view.GetParent());
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
}
