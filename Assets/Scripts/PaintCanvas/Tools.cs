using System;
using UnityEngine;
using UnityEngine.UI;

public class Tools : View {
      [SerializeField] private Toggle lineToggle, arrowToggle;
      [SerializeField] private Button acceptButton, cancelButton, undoButton ,screenShot;
      public ColorMenu colorMenu;

      public event Action AcceptClickedEvent, RejectClickedEvent, UndoEvent, LineSelectedEvent, ArrowSelectedEvent , ScreenShotEvent;

      private void OnEnable(){
            acceptButton.onClick.AddListener(delegate { AcceptClickedEvent?.Invoke(); });
            cancelButton.onClick.AddListener(delegate { RejectClickedEvent?.Invoke(); });
            undoButton.onClick.AddListener(delegate { UndoEvent?.Invoke(); });

            lineToggle.onValueChanged.AddListener(delegate { LineSelectedEvent?.Invoke(); });
            arrowToggle.onValueChanged.AddListener(delegate { ArrowSelectedEvent?.Invoke(); });
            
            screenShot.onClick.AddListener(delegate { ScreenShotEvent?.Invoke(); });
      }

      private void OnDisable(){
            acceptButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
            undoButton.onClick.RemoveAllListeners();
            lineToggle.onValueChanged.RemoveAllListeners();
            arrowToggle.onValueChanged.RemoveAllListeners();
            
            screenShot.onClick.RemoveAllListeners();
      }
}