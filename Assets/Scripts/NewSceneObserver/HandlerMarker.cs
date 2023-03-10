using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class HandlerMarker
{
    [SerializeField] private Marker3D prefabMarker3D;
    [SerializeField] private ObjectOrientation _objectOrientation;
    private List<Marker3D> _markers3D = new List<Marker3D>();
    private Color _color = Color.red;
    private Marker3D _currentMarker3D;

    public event Action<Marker3D> SelectObjectEvent;

    public void Initialize() {
        _objectOrientation = new ObjectOrientation();
        _objectOrientation.Initialize();
    }

    public void SetColor(Color color) {
        _currentMarker3D.SetColor(color);
        _color = color;
    }
    
    
    public void CreateMarker(Vector2 position) {
        Marker3D marker3D = Object.Instantiate(prefabMarker3D);
        Vector3 positionMarker = _objectOrientation.GetPositionAR(position);
        marker3D.Initialize();
        marker3D.SelectObjectEvent += SelectMarker3D;
        marker3D.SetPosition(positionMarker);
        marker3D.SetColor(_color);
        marker3D.SetCount(_markers3D.Count + 1);
        _markers3D.Add(marker3D);
    }

    private void SelectMarker3D(Marker3D marker3D) {
        _currentMarker3D = marker3D;
        SelectObjectEvent?.Invoke(marker3D);
    }
    
    
    public void DeleteMarker() {
        _markers3D.Remove(_currentMarker3D);
        Object.Destroy(_currentMarker3D.gameObject);
        _currentMarker3D = null;
        CalculateCountMarker();
    }

    private void CalculateCountMarker() {
        for(int i = 0 ; i < _markers3D.Count; i++)
        {
            _markers3D[i].SetCount(i + 1);
        }
    }

    public void Clear(){
        foreach (var marker3D in _markers3D)
            Object.Destroy(marker3D.gameObject);
        _markers3D.Clear();
    }
}
