using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenu : ViewBase
{
    [SerializeField] private ViewColorMenu colorMenu;

    [SerializeField] private Button
        buttonMicOn,
        buttonMicOff,
        buttonCameraOn,
        buttonCameraOff,
        buttonColorMenu;


    public event Action
        MicOnEvent,
        MicOffEvent,
        CameraOnEvent,
        CameraOffEvent,
        ColorMenuEvent;
    
    public event Action<Color> ChangeColorEvent;
    

    private void Awake()
    {
        MicOnEvent += MicOn;
        MicOffEvent += MicOff;
        CameraOnEvent += CameraOn;
        CameraOffEvent += CameraOff;
        ColorMenuEvent += ColorMenu;
        colorMenu.ChangeColorEvent += ChangeColorButton;
    }


    private void Start()
    {
        colorMenu.Close();
        Close();
    }

    private void OnEnable()
    {
        buttonMicOn.onClick.AddListener(delegate { MicOnEvent?.Invoke(); });
        buttonMicOff.onClick.AddListener(delegate { MicOffEvent?.Invoke(); });
        buttonCameraOn.onClick.AddListener(delegate { CameraOnEvent?.Invoke(); });
        buttonCameraOff.onClick.AddListener(delegate { CameraOffEvent?.Invoke(); });
        buttonColorMenu.onClick.AddListener(delegate { ColorMenuEvent?.Invoke(); });
    }

    private void OnDisable()
    {
        buttonMicOn.onClick.RemoveAllListeners();
        buttonMicOff.onClick.RemoveAllListeners();
        buttonCameraOn.onClick.RemoveAllListeners();
        buttonCameraOff.onClick.RemoveAllListeners();
        buttonColorMenu.onClick.RemoveAllListeners();
    }

    private void MicOn()
    {
        buttonMicOn.gameObject.SetActive(false);
        buttonMicOff.gameObject.SetActive(true);
    }

    private void MicOff()
    {
        buttonMicOn.gameObject.SetActive(true);
        buttonMicOff.gameObject.SetActive(false);
    }

    private void CameraOn()
    {
        buttonCameraOn.gameObject.SetActive(false);
        buttonCameraOff.gameObject.SetActive(true);
    }

    private void CameraOff()
    {
        buttonCameraOn.gameObject.SetActive(true);
        buttonCameraOff.gameObject.SetActive(false);
    }

    private void ColorMenu()
    {
        if (!colorMenu.gameObject.activeInHierarchy)
            colorMenu.Open();
        else
            colorMenu.Close();
    }

    private void ChangeColorButton(Color color)
    {
        buttonColorMenu.image.color = color;
        ChangeColorEvent?.Invoke(color);
        colorMenu.Close();
    }
}