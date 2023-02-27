using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming;
using TMPro;
using System.Threading.Tasks;

public class HandlerMessage : MonoBehaviour
{
    [SerializeField] private RenderController renderController;
    [SerializeField] private SingleConnection connection;
    [SerializeField] private InputSenderData inputSenderData;
    [SerializeField] private InputReceiverData inputReceiverData;

    [SerializeField] private TMP_Text textLocalId;

    [SerializeField] private TMP_Text textInMessage;
    [SerializeField] private TMP_InputField inputMessage;
    [SerializeField] private Button buttonSendMessage;

    [SerializeField] private GameObject panelButtonCloseCall;
    [SerializeField] private Button buttonOpenCall;
    [SerializeField] private Button buttonCloseCall;

    [SerializeField] private GameObject panelCallUp;
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private Button buttonCallUp;

    [SerializeField] private GameObject panelInComingCall;
    [SerializeField] private TMP_Text infoComingCall;
    [SerializeField] private Button buttonTakeCall;
    [SerializeField] private Button buttonResetCall;

    [SerializeField] private string localId;
    [SerializeField] private string remoteId;
    [SerializeField] private string textOutMessage;
    [SerializeField] private string tempRemoteId;

    [SerializeField] private bool isStartSender;


    private void Start() {
        localId = UnityEngine.Random.Range(100, 999).ToString();
        textLocalId.text = localId;
        AsyncAwake();
    }

    private async void AsyncAwake() {
        await Task.Delay(TimeSpan.FromSeconds(1.0f));
        Debug.Log("START HandlerMessage");
        
        inputReceiverData.OnMessageEvent += InComingMessage;
        inputId.onValueChanged.AddListener(input => remoteId = input);
        inputMessage.onValueChanged.AddListener(input => textOutMessage = input);
        buttonSendMessage.onClick.AddListener(() => OnSendMessage());

        buttonCallUp.onClick.AddListener(() => OnCallUp());
        
        buttonTakeCall.onClick.AddListener(() => OnTakeCallUp());
        buttonResetCall.onClick.AddListener(() => OnResetCallUp());
        buttonTakeCall.interactable = false;
        buttonResetCall.interactable = false;

        buttonOpenCall.onClick.AddListener(() => OnOpenCall());
        buttonCloseCall.onClick.AddListener(() => OnCloseCall());
        buttonOpenCall.gameObject.SetActive(true);
        buttonCloseCall.gameObject.SetActive(false);


        panelInComingCall.SetActive(false);

        CreateLocalConnection();

        inputSenderData.OnStartedChannel += id => {
            isStartSender = true;
            buttonSendMessage.interactable = true;
        };

        inputSenderData.OnStoppedChannel += id => {
            isStartSender = false;
        };
    }

    private void CreateLocalConnection() {
        connection.CreateConnection(localId);
    }

    private void CreateRemoteConnection() {
        connection.CreateConnection(remoteId);
    }

    private void DeleteLocalConnection() {
        connection.DeleteConnection(localId);
    }

    private void DeleteRemoteConnection() {
            connection.DeleteConnection(remoteId);
    }

    private void OnOpenCall()
    {
        panelCallUp.SetActive(true);
        buttonCallUp.interactable = true;
        buttonOpenCall.gameObject.SetActive(false);
        buttonCloseCall.gameObject.SetActive(true);
    }

    private void OnCloseCall()
    {
        panelCallUp.SetActive(false);
        buttonOpenCall.gameObject.SetActive(true);
        buttonCloseCall.gameObject.SetActive(false);
        OnResetCallUp();
    }

    private void OnSendMessage() {
        if (string.IsNullOrEmpty(remoteId)) return;
        if (string.IsNullOrEmpty(textOutMessage)) return;
        Debug.Log("Пробуем отправить");
        if (isStartSender == false)
        {
            DeleteLocalConnection();
            CreateRemoteConnection();
        }
        buttonSendMessage.interactable = false;
        buttonCallUp.interactable = false;
        SendPrivateMessage();
    }


    private void OnCallUp() {
        if (string.IsNullOrEmpty(remoteId)) return;
        Debug.Log("Пробуем дозвонится");
        if (isStartSender == false)
        {
            DeleteLocalConnection();
            CreateRemoteConnection();
        }
        buttonCallUp.interactable = false;
        panelCallUp.SetActive(false);
        buttonSendMessage.interactable = false;
        SendCall();
    }


    private void OnTakeCallUp() {
        buttonTakeCall.interactable = false;
        string text = $"Соединение\n  ID: {tempRemoteId}";
        infoComingCall.text = text;
        SendAnswerCall();
        StartMainStream();
        panelInComingCall.SetActive(false);
        buttonOpenCall.gameObject.SetActive(false);
        buttonCloseCall.gameObject.SetActive(true);
    }

    private void OnResetCallUp() {
        string text = $"Вызов отклонен";
        infoComingCall.text = text;
        StopMainStream();
        buttonTakeCall.interactable = false;
        buttonResetCall.interactable = false;
        panelInComingCall.SetActive(false);
    }


    private void SendCall() {
        MessageData messageData = new MessageData();
        messageData.id = localId;
        messageData.typeMessage = TypeMessage.Call;
        messageData.roomId = UnityEngine.Random.Range(10000, 99999).ToString();
        string message = JsonUtility.ToJson(messageData);
        Send(message);
    }

    private void SendAnswerCall() {
        MessageData messageData = new MessageData();
        messageData.id = localId;
        messageData.typeMessage = TypeMessage.AnswerCall;
        messageData.roomId = tempRemoteId;
        string message = JsonUtility.ToJson(messageData);
        Send(message);
    }


    private void SendPrivateMessage() { 
        MessageData messageData = new MessageData();
        messageData.id = localId;
        messageData.typeMessage = TypeMessage.PrivateMessage;
        messageData.textMessage = textOutMessage;
        string message = JsonUtility.ToJson(messageData);
        Send(message);
    }

    private async void Send(string message) {
        while (!isStartSender)
        {
            await Task.Yield();
        }
        Debug.Log("Данные пошли");
        inputSenderData.Send(message);
        buttonSendMessage.interactable = true;
        buttonCallUp.interactable = true;
    }

    private void InComingMessage(byte[] bytes) {
        Debug.Log("Входящие данные");
        string message = System.Text.Encoding.UTF8.GetString(bytes);
        ParseMessage(message);
    }

    private void ParseMessage(string message) {
        MessageData messageData = JsonUtility.FromJson<MessageData>(message);
        switch (messageData.typeMessage)
        {
            case TypeMessage.Call:
                InComingCall(messageData);
                break;
            case TypeMessage.AnswerCall:
                TakeAnswer(messageData);
                break;
            case TypeMessage.PrivateMessage:
                TakePrivateMessage(messageData);
                break;
        }
    }

    private void InComingCall(MessageData messageData) {
        string text = $"Входящий вызов\n от {messageData.id}";
        panelInComingCall.SetActive(true);
        infoComingCall.text = text;
        tempRemoteId = messageData.roomId;
        buttonTakeCall.interactable = true;
        buttonResetCall.interactable = true;
    }

    private void TakeAnswer(MessageData messageData) {
        tempRemoteId = messageData.roomId;
        string text = $"Соединение\n  ID: {tempRemoteId}";
        infoComingCall.text = text;
        buttonResetCall.interactable = true;
        StartMainStream();
    }

    private void TakePrivateMessage(MessageData messageData) {
        string text = $"Сообщение от {messageData.id}\n{messageData.textMessage}";
        inputId.text = localId;
        textInMessage.text = text;
    }

    private void StartMainStream() {
        renderController.SetConnectId(tempRemoteId);
        renderController.CallUp();
    }

    private void StopMainStream() {
        renderController.HangUp();
    }
}

