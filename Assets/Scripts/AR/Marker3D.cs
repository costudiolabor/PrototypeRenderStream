using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class Marker3D : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private TextMesh textCount;
    [SerializeField] private TMP_Text textCount;
    [SerializeField] private MeshRenderer meshRenderer;
    private Camera _camera;
    private string _description;
    private string _textMarker = "";
    
    public event Action<Sticker> OpenStickerEvent;
    public event Action<Sticker> DeleteStickerEvent;


    public void Initialize() {
    }
    
    public void SetPosition(Vector3 position) => transform.position = position;
    
    public void SetColor(Color color) => meshRenderer.material.color = color;

    public void SetCountText(int count)
    {
        textCount.text = count.ToString();
    }

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public void SetDescriptionText(string description)
    {
        _description = description;
    }
    
    public string GetDescription()
    {
        return _description;
    }
    
    public void OpenSticker() {
        //OpenStickerEvent?.Invoke(this);
    }

    public void SetTextSticker(string textMarker) {
        _textMarker = textMarker;
    }
   
    public string GetTextSticker() {
        return _textMarker;
    }

    private void DeleteSticker() {
       // DeleteStickerEvent?.Invoke(this);
        //Destroy(gameObject);
    }
    
    
    private void Update()
    {
        if (_camera)transform.LookAt(_camera.transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ClickMarker  " + textCount.text);
    }
}