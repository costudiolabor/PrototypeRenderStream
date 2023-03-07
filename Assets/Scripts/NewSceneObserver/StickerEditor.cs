using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class StickerEditor
{
    [SerializeField] private Sticker prefabSticker;
   

    private List<Sticker> _stickers = new List<Sticker>();
    private RectTransform _linesParent;
    private Color _color = Color.red;

    public event Action<Sticker> OpenStickerEvent;

    public void Initialize(RectTransform linesParent) {
        _linesParent = linesParent;
    }

    public void SetColor(Color color) =>_color = color;
    public void CreateSticker(Vector2 rectPoint) {
       Sticker sticker = Object.Instantiate(prefabSticker, _linesParent);
       sticker.Initialize();
       sticker.SetPosition(rectPoint);
       sticker.SetColor(_color);
       sticker.SetCountText(_stickers.Count + 1);
       sticker.DeleteStickerEvent += DeleteSticker;
       sticker.OpenStickerEvent += OpenStickerEvent;
       _stickers.Add(sticker);
    }
    public void DeleteSticker(Sticker sticker) {
        _stickers.Remove(sticker);
        CalculateCountSticker();
    }

    private void CalculateCountSticker() {
        for(int i = 0 ; i < _stickers.Count; i++)
        {
            _stickers[i].SetCountText(i + 1);
        }
    }

    public void Clear(){
        foreach (var sticker in _stickers)
            Object.Destroy(sticker.gameObject);
        _stickers.Clear();
    }
}
