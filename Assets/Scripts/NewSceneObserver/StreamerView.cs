using System;
using UnityEngine;
using UnityEngine.UI;

public class StreamerView : AnimatedView
{
    [SerializeField] private Button callUpButton;
    [SerializeField] private Button hangUpButton;
    [SerializeField] private RawImage localVideoImage;
    [SerializeField] private RawImage remoteVideoImage;
   
    public event Action CallUpEvent, HangUpEvent;

    private void Awake() {
        callUpButton.onClick.AddListener(() => CallUpEvent?.Invoke());
        hangUpButton.onClick.AddListener(() => HangUpEvent?.Invoke());
        CallUpEvent += OnCallUpEvent;
        HangUpEvent += OnHangUpEvent;
        OnHangUpEvent();
    }

    private void OnCallUpEvent() {
        callUpButton.gameObject.SetActive(false);
        hangUpButton.gameObject.SetActive(true);
    }
    
    private void OnHangUpEvent() {
        callUpButton.gameObject.SetActive(true);
        hangUpButton.gameObject.SetActive(false);
    }
    
    public  Texture localVideoTexture {
        set => localVideoImage.texture = value;
    }
    
    public  Texture remoteVideoTexture {
        set => remoteVideoImage.texture = value;
    }
  
}

