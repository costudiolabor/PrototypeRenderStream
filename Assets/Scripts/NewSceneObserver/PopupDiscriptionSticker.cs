using System;
using UnityEngine;
using UnityEngine.UI;


public class PopupDiscriptionSticker : View
{
    [SerializeField] private Button buttonCloseDescriptionSticker;
    [SerializeField] private InputField inputFieldDescription;
    [SerializeField] private Text textDescription;

    private Sticker _sticker;

    public void Awake() {
        buttonCloseDescriptionSticker.onClick.AddListener(ClosePanel);
        inputFieldDescription.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        Close();
    }

    public void SetTextPopup(Sticker sticker) {
        _sticker = sticker;
        inputFieldDescription.text = sticker.GetTextSticker().ToString();
    }

    private void ValueChangeCheck() {
        _sticker.SetTextSticker(textDescription.text.ToString());
    }

    private void ClosePanel() {
        _sticker.SetTextSticker(textDescription.text.ToString());
        Close();
    }
    
}
