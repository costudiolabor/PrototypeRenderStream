using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorToggle : MonoBehaviour
{
    [SerializeField] private Image image;
    private Toggle _toggle;

    public event Action<Color> ChangeColorEvent;


    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate
        {
           // Debug.Log("ChangeColor = ");
            //if (_toggle.isOn) 
                ChangeColorEvent?.Invoke(image.color);
        });
    }
}