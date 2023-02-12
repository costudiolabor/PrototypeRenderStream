using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupDiscriptionSticker : View
{
    [SerializeField] private Button buttonCloseDiscriptionSticker;
    [SerializeField] private InputField inputFieldDiscription;
    [SerializeField] private Text textDiscription;

    public void Awake() {
        buttonCloseDiscriptionSticker.onClick.AddListener(Close);
        Close();
    }

    public void SetTextPopup(string textSticker)
    {
        textDiscription.text = "";
        textDiscription.text = textSticker.ToString();
    }


}
