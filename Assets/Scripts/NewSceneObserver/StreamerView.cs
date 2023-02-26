using TMPro;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class StreamerView : AnimatedView
{
    [SerializeField] private Button callUpButton;
    [SerializeField] private Button hangUpButton;
    [SerializeField] private RawImage localVideoImage;
    [SerializeField] private RawImage remoteVideoImage;
    [SerializeField] private Button buttonCloseOpenImage;
    [SerializeField] private RectTransform panelNotice;
    [SerializeField] private TMP_Text textNotice;
    [SerializeField] private AudioSource audioCallExpert;
    [SerializeField] private float timeCallExpert;

    private Coroutine _refCallExpert;
    
    public event Action CallUpEvent, HangUpEvent;

    public  Texture localVideoTexture {
        set => localVideoImage.texture = value;
    }
    
    public  Texture remoteVideoTexture {
        set => remoteVideoImage.texture = value;
    }

    private void Awake() {
        callUpButton.onClick.AddListener(() => CallUpEvent?.Invoke());
        hangUpButton.onClick.AddListener(() => HangUpEvent?.Invoke());
        buttonCloseOpenImage.onClick.AddListener(CloseOpenRemoveImage); 
        callUpButton.gameObject.SetActive(true);
        hangUpButton.gameObject.SetActive(false); 
        CallUpEvent += OnCallUp;
        HangUpEvent += OnHangUp;
    }

    private void  CloseOpenRemoveImage() {
        remoteVideoImage.gameObject.SetActive(!remoteVideoImage.gameObject.activeInHierarchy);
    }
    
    public void SetNotice(string text) {
      panelNotice.gameObject.SetActive(true);
      textNotice.text = text;
    }

    public void StartInputReceiver(string id) {
        Disconnect();
    }
    
    public void StoppedInputReceiver(string id) {
        OnHangUp();
        SetNotice("Абонент не отвечает \n\n ID: " + id);
    }

    private void OnCallUp() {
        callUpButton.gameObject.SetActive(false);
        hangUpButton.gameObject.SetActive(true);
        panelNotice.gameObject.SetActive(false);
        Connect();
    }
    
    private void OnHangUp() {
        callUpButton.gameObject.SetActive(true);
        hangUpButton.gameObject.SetActive(false); 
        Disconnect();
        SetNotice("Абонент не отвечает");
    }

    private void Connect() {
        //audioCallExpert.Play();
        _refCallExpert = StartCoroutine(CallExpert()) ;
    }

    private void Disconnect() {
        audioCallExpert.Stop();
        StopCoroutine(_refCallExpert);
    }
    

    IEnumerator CallExpert() {
        yield return new WaitForSeconds(timeCallExpert);
        //HangUpEvent?.Invoke();
    }
}

