using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScreenShotImage : MonoBehaviour, IPointerClickHandler
{
   [SerializeField] private RawImage image;
   [SerializeField] private UISwipeDelete uiSwipeDelete;
   private Texture _texture;

   public event Action<Texture> ClickImageEvent;
   public event Action<ScreenShotImage> DestroyImageEvent;

   public void SetTexture(Texture texture) {
      this._texture = texture;
      image.texture = texture;
      uiSwipeDelete.DestroyImageEvent += DestroyScreenShotImage;
   }
   
   public void OnPointerClick(PointerEventData eventData) {
      ClickImageEvent?.Invoke(_texture);
   }

   private void DestroyScreenShotImage() {
      DestroyImageEvent?.Invoke(this);
      ClickImageEvent = null;
   }
}
