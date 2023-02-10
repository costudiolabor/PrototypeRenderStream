using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class StickerModel
{
    [SerializeField] private Sticker prefabSticker;
    [SerializeField] private List<Sticker> _stickers = new List<Sticker>();

    private  RectTransform linesParent;
    private Color _color = Color.red;

    public  void Initialize(RectTransform linesParent) => this.linesParent = linesParent;

    public void SetColor(Color color) =>_color = color;
    
    public void CreateSticker(Vector2 rectPoint) {
       Sticker sticker = Object.Instantiate(prefabSticker, linesParent);
       sticker.Initialize();
       sticker.SetPosition(rectPoint);
       sticker.SetColorImage(_color);
       sticker.SetSampleText(_stickers.Count + 1);
       sticker.DeleteStickerEvent += DeleteSticker;
       _stickers.Add(sticker);
    }

    public void DeleteSticker(Sticker sticker)
    {
        Debug.Log("Del Sticker");
        _stickers.Remove(sticker);
    }
}
