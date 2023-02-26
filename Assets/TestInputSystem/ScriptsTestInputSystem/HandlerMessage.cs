using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.RenderStreaming;
using TMPro;
using System.Threading.Tasks;

public class HandlerMessage : MonoBehaviour
{
    [SerializeField] private SingleConnection connection;
    [SerializeField] private InputSenderData InputSenderData;
    [SerializeField] private InputReceiverData InputReceiverData;

    [SerializeField] private TMP_Text textLocalId;
    [SerializeField] private TMP_Text textInMessage;
    [SerializeField] private TMP_InputField connectionIdInput;
    [SerializeField] private TMP_InputField inputMessage;
    [SerializeField] private Button sendMessage;


    [SerializeField] private string localId;
    [SerializeField] private string remoteId;
    [SerializeField] private string textMessage;

    [SerializeField] private bool isRemoteConnect;


    private  void Awake()
    {
        textLocalId.text = localId;
        AsyncAwake();
    }

    private async void AsyncAwake()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        Debug.Log("START HandlerMessage");

        InputReceiverData.OnMessageEvent += InComingMessage;

        connectionIdInput.onValueChanged.AddListener(input => remoteId = input);

        inputMessage.onValueChanged.AddListener(input => textMessage = input);

        sendMessage.onClick.AddListener(() => OnSendMessage());

        CreateLocalConnection();


    }




    private void CreateLocalConnection()
    {
        connection.CreateConnection(localId);
    }

    private void CreateRemoteConnection()
    {
        connection.CreateConnection(remoteId);
    }

    private void DeleteLocalConnection()
    {
        connection.DeleteConnection(localId);
    }

    private void DeleteRemoteConnection()
    {
        connection.DeleteConnection(remoteId);
    }


    private void OnSendMessage()
    {
        if (string.IsNullOrEmpty(remoteId)) return;

        if (isRemoteConnect == false)
        {
            DeleteLocalConnection();
            CreateRemoteConnection();
        }

        sendMessage.interactable = false;

        Debug.Log("Пробуем отправить");
        InputSenderData.OnStartedChannel += id =>
        {
            isRemoteConnect = true;
            SendMessage();
            sendMessage.interactable = true;
        };

        if (isRemoteConnect == true)
        {
            SendMessage();
            sendMessage.interactable = true;
        }
    }


    private void SendMessage()
    {
        if (string.IsNullOrEmpty(textMessage)) return;
        Debug.Log("Сообщение пошло");
        InputSenderData.Send(textMessage);
    }

    private void InComingMessage(byte[] bytes)
    {
        Debug.Log("Входящее Сообщение");
        string message = System.Text.Encoding.UTF8.GetString(bytes);
        textInMessage.text = message;
    }
}

