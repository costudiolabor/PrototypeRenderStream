using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenShotImage : MonoBehaviour, IPointerClickHandler
{
   [SerializeField] private RawImage image;
   private Texture _texture2D;

   public event Action<Texture> ClickImageEvent;
   
   public void SetTexture(Texture texture)
   {
      _texture2D = texture;
      image.texture = texture;
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
      ClickImageEvent?.Invoke(_texture2D);
      //Debug.Log("Click");
   }

   private void OnDestroy()
   {
      ClickImageEvent = null;
   }
}
