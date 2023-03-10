using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class Editor3DMarkerView : AnimatedView, IPointerDownHandler
{
    [SerializeField] private GameObject panelEditMarker;
    [SerializeField] private TMP_InputField inputDescription;
    [SerializeField] private ColorMenu colorMenu;
    [SerializeField] private Button buttonDelete;
    [SerializeField] private Button buttonClose;
    
    public event Action<Color> ColorChangedEvent;
    public event Action<Vector2> PointerDownEvent;
    public event Action DeleteMarker3DEvent; 

    public void Initialize() {
        panelEditMarker.SetActive(false);
        inputDescription.onValueChanged.AddListener(OnChangeInputText );
        buttonDelete.onClick.AddListener(OnButtonDelete);
        buttonClose.onClick.AddListener(CloseEditor3DMarkerView);
        colorMenu.ColorChangedEvent += OnColorChanged;
    }

    private void CloseEditor3DMarkerView()
    {
        Close();
    }
    
    public void OpenPanelEditMarker()
    {
        panelEditMarker.SetActive(true);
    }
    
    private void ClosePanelEditMarker()
    {
        panelEditMarker.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("SetMarker: " + eventData.position);
        PointerDownEvent?.Invoke(eventData.position);
    }

    private void OnChangeInputText(string text)
    {
        
    }

    private void OnColorChanged(Color color)
    {
        ColorChangedEvent?.Invoke(color);
    }
    
    private void OnButtonDelete()
    {
        DeleteMarker3DEvent?.Invoke();
        ClosePanelEditMarker();
    }

}
