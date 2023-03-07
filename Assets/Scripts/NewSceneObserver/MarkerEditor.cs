using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class MarkerEditor
{
    [SerializeField] private Marker3D prefabMarker3D;
    [SerializeField] private ObjectOrientation _objectOrientation;
    private List<Marker3D> _markers3D = new List<Marker3D>();
    private Color _color = Color.red;

    public event Action<Sticker> OpenStickerEvent;

    public void Initialize()
    {
        _objectOrientation = new ObjectOrientation();
        _objectOrientation.Initialize();
        SetColor(_color);
    }

    public void SetColor(Color color) =>_color = color;
    
    public void CreateSticker(Vector2 position) {
        Marker3D marker3D = Object.Instantiate(prefabMarker3D);
        Vector3 positionMarker = _objectOrientation.GetPositionAR(position);
        marker3D.Initialize();
        marker3D.SetPosition(positionMarker);
        marker3D.SetColor(_color);
        marker3D.SetCountText(_markers3D.Count + 1);
        //marker3D.DeleteStickerEvent += DeleteSticker;
        marker3D.OpenStickerEvent += OpenStickerEvent;
        _markers3D.Add(marker3D);
    }
    
    public void DeleteSticker(Marker3D marker3D) {
        _markers3D.Remove(marker3D);
        CalculateCountSticker();
    }

    private void CalculateCountSticker() {
        for(int i = 0 ; i < _markers3D.Count; i++)
        {
            _markers3D[i].SetCountText(i + 1);
        }
    }

    public void Clear(){
        foreach (var sticker in _markers3D)
            Object.Destroy(sticker.gameObject);
        _markers3D.Clear();
    }
}
