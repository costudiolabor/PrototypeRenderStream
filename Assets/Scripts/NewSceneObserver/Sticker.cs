using System;
using UnityEngine;
using UnityEngine.UI;


public class Sticker : MonoBehaviour
{
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private Image _sampleImage;
   [SerializeField] private Text countText;
   [SerializeField] private Button buttonSticker;
   [SerializeField] private Button buttonDeleteSticker;

   private string _textSticker = "";
   public event Action<Sticker> OpenStickerEvent;
   public event Action<Sticker> DeleteStickerEvent;

   public void Initialize() {
      buttonSticker.onClick.AddListener(OpenSticker);
      buttonDeleteSticker.onClick.AddListener(DeleteSticker);
   }
   
   public void SetPosition(Vector2 rectPoint) => _rectTransform.localPosition = rectPoint;

   public void SetColorImage(Color color) => _sampleImage.color = color;

   public void SetCountText(int count) => countText.text = count.ToString();

   public void OpenSticker() {
        OpenStickerEvent?.Invoke(this);
    }

   public void SetTextSticker(string textSticker) {
      _textSticker = textSticker;
   }
   
   public string GetTextSticker() {
      return _textSticker;
   }

   private void DeleteSticker() {
      DeleteStickerEvent?.Invoke(this);
      Destroy(gameObject);
   }
}
