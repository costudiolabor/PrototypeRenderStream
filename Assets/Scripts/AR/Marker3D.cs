using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class Marker3D : MonoBehaviour, IMarker3D
{
    [SerializeField] private TMP_Text textCount;
    [SerializeField] private MeshRenderer meshRenderer;
    private Camera _camera;
    private string _description = "";
   // private string _textMarker = "";
    
    public event Action<Marker3D> SelectObjectEvent;

    public void Initialize() {
    }
    
    public void SetPosition(Vector3 position) => transform.position = position;
    
    public void SetColor(Color color) => meshRenderer.material.color = color;

    public void SetCount(int count)
    {
        textCount.text = count.ToString();
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public void SetDescription(string description)
    {
        _description = description;
    }
    
    public string GetDescription()
    {
        return _description;
    }

    private void Update() {
        if (_camera)transform.LookAt(_camera.transform);
    }


    public void SelectObject() {
        SelectObjectEvent?.Invoke(this);
    }
}