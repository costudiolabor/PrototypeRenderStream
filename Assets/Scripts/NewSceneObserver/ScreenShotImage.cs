using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenShotImage : MonoBehaviour, IPointerClickHandler
{
   [SerializeField] private RawImage image;
   private Texture _texture;

   public event Action<Texture> ClickImageEvent;
   
   public void SetTexture(Texture texture) {
      this._texture = texture;
      image.texture = texture;
   }
   
   public void OnPointerClick(PointerEventData eventData) {
      ClickImageEvent?.Invoke(_texture);
   }

   private void OnDestroy() {
      ClickImageEvent = null;
   }
}
