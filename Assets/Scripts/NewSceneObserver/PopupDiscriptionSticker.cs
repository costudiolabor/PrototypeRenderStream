using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopupDiscriptionSticker : View
{
    [SerializeField] private Button buttonCloseDescriptionSticker;
    [SerializeField] private  TMP_InputField inputFieldDescription;
    [SerializeField] private TMP_Text textDescription;
    private const int  KeyCodeBackSpace = (int)KeyCode.Backspace;

    private Sticker _sticker;

    public void Awake() {
        buttonCloseDescriptionSticker.onClick.AddListener(ClosePanel);
        inputFieldDescription.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        Close();
    }

    public void SetCharInputField(char inputChar) {
        CheckCorrectInput(inputChar);
    }

    public void SetTextPopup(Sticker sticker) {
        _sticker = sticker;
        inputFieldDescription.text = sticker.GetTextSticker().ToString();
    }

    private void ValueChangeCheck() {
        if (!_sticker) return;
        _sticker.SetTextSticker(textDescription.text.ToString());
    }

    private void ClosePanel() {
        _sticker.SetTextSticker(textDescription.text.ToString());
        Close();
    }

    private void CheckCorrectInput(char inputChar) {
        if ((int)inputChar == KeyCodeBackSpace) {
            if (inputFieldDescription.text.Length == 0) return;
            var currentText = inputFieldDescription.text; 
            var lastChar = inputFieldDescription.text.Length - 1;
            inputFieldDescription.text = currentText.Remove(lastChar);
        }
        else 
            inputFieldDescription.text += inputChar;
    }
}
