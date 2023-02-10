using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sticker : MonoBehaviour
{
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private Image _sampleImage;
   [SerializeField] private Text sampleText;
   [SerializeField] private Button buttonDeleteSticker;

   public event Action<Sticker> DeleteStickerEvent;

   public void Initialize()
   {
      buttonDeleteSticker.onClick.AddListener(DeleteSticker);
   }
   
   public void SetPosition(Vector2 rectPoint) => _rectTransform.localPosition = rectPoint;

   public void SetColorImage(Color color) => _sampleImage.color = color;

   public void SetSampleText(int count) => sampleText.text = count.ToString();

   private void DeleteSticker()
   {
      DeleteStickerEvent?.Invoke(this);
      Destroy(gameObject);
   }
   
}
