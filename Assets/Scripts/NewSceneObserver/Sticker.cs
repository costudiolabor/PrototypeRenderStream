using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sticker : MonoBehaviour
{
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private Image _sampleImage;
   [SerializeField] private Text countText;
   [SerializeField] private Button buttonSticker;
   [SerializeField] private Button buttonDeleteSticker;

    [SerializeField] private string textSticker = "";
   

   public event Action<string> OpenStickerEvent;
   public event Action<Sticker> DeleteStickerEvent;

   public void Initialize()
   {
      buttonSticker.onClick.AddListener(delegate { GetTextSticker(); });
      buttonDeleteSticker.onClick.AddListener(DeleteSticker);
   }
   
   public void SetPosition(Vector2 rectPoint) => _rectTransform.localPosition = rectPoint;

   public void SetColorImage(Color color) => _sampleImage.color = color;

   public void SetCountText(int count) => countText.text = count.ToString();

   public void GetTextSticker() {
        OpenStickerEvent?.Invoke(textSticker);
    }

   private void DeleteSticker()
   {
      DeleteStickerEvent?.Invoke(this);
      Destroy(gameObject);
   }
   
}
