using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Editor3DMarkerView : AnimatedView, IPointerDownHandler
{
    [SerializeField] private GameObject panelEditMarker;
    [SerializeField] private TMP_InputField inputDescription;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private ColorMenu colorMenu;
    [SerializeField] private Button buttonDelete;
    [SerializeField] private Button buttonClose;
    private const int  KeyCodeBackSpace = (int)KeyCode.Backspace;

    
    private Marker3D _marker3D;
    
    public event Action<Color> ColorChangedEvent;
    public event Action<Vector2> PointerDownEvent;
    public event Action DeleteMarker3DEvent; 

    public void Initialize() {
        panelEditMarker.SetActive(false);
        inputDescription.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        buttonDelete.onClick.AddListener(OnButtonDelete);
        buttonClose.onClick.AddListener(CloseView);
        colorMenu.ColorChangedEvent += OnColorChanged;
    }
    private void ValueChangeCheck() {
        if (!_marker3D) return;
        _marker3D.SetDescription(textDescription.text.ToString());
    }
    
    private void CloseView() {
        Close();
    }
    
    public void OpenPanelEditMarker(Marker3D marker3D) {
        panelEditMarker.SetActive(true);
        SetTextInputDescription(marker3D);
    }
    
    private void ClosePanelEditMarker() {
        panelEditMarker.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData) {
        PointerDownEvent?.Invoke(eventData.position);
    }

    private void OnColorChanged(Color color) {
        ColorChangedEvent?.Invoke(color);
    }
    
    private void OnButtonDelete() {
        DeleteMarker3DEvent?.Invoke();
        ClosePanelEditMarker();
    }
    public void SetTextInputDescription(Marker3D marker3D) {
        _marker3D = marker3D;
        inputDescription.text = marker3D.GetDescription().ToString();
    }
    
    public void SetCharInputField(char inputChar) {
        CheckCorrectInput(inputChar);
    }
    
    private void CheckCorrectInput(char inputChar) {
        if ((int)inputChar == KeyCodeBackSpace) {
            if (inputDescription.text.Length == 0) return;
            var currentText = inputDescription.text; 
            var lastChar = inputDescription.text.Length - 1;
            inputDescription.text = currentText.Remove(lastChar);
        }
        else 
            inputDescription.text += inputChar;
    }

}
