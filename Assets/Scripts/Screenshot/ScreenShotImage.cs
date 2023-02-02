using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenShotImage : MonoBehaviour, IPointerClickHandler
{
   [SerializeField] private RawImage image;
   private Texture2D _texture2D;

   public event Action<Texture2D> ClickImageEvent;
   
   public void SetTexture(Texture2D texture)
   {
      _texture2D = texture;
      image.texture = texture;
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
      ClickImageEvent?.Invoke(_texture2D);
      Debug.Log("Click");
   }

   private void OnDestroy()
   {
      ClickImageEvent = null;
   }
}
