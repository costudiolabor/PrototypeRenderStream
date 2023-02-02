using System;
using UnityEngine;
using NotificationSamples;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationsManager;
    private int _notificationDelay = 5;

    private void Start()
    {
        InitializeNotifications();
    }

    private void InitializeNotifications()
    {
        GameNotificationChannel channel =
            new GameNotificationChannel("mnTutorial", "Mobile Notifications Tutorial", "Just a notification");
        StartCoroutine(notificationsManager.Initialize(channel));
      
    }

    public void OnTimeInput(string text)
    {
        if (int.TryParse(text, out int sec))
            _notificationDelay = sec;
        Debug.Log("sec = " + sec);
    }

    public void CreateNotification()
    {
        Debug.Log("Crete Notification");
        CreateNotification("Mobile Notifications Tutorial", "Привет я Уведомление", DateTime.Now.AddSeconds(_notificationDelay));
    }

    private void CreateNotification(string title, string body, DateTime time)
    {
        IGameNotification notification = notificationsManager.CreateNotification();
        if (notification != null)
        {
            notification.Title = title;
            notification.Body = body;
            notification.DeliveryTime = time;
            //notification.SmallIcon = "icon";
            notificationsManager.ScheduleNotification(notification);
        }
    }
    
}