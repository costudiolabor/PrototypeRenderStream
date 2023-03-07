using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : ViewBase
{
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button undoButton;

    public event Action CancelButtonEvent;
    public event Action SaveButtonEvent;
    public event Action UndoButtonEvent;

    private void Awake()
    {
        cancelButton.onClick.AddListener(delegate
        {
            CancelButtonEvent?.Invoke();
            Close();
        });

        saveButton.onClick.AddListener(delegate
        {
            SaveButtonEvent?.Invoke();
            Close();
        });

        undoButton.onClick.AddListener(delegate { UndoButtonEvent?.Invoke(); });
    }


    private void OnDestroy()
    {
        cancelButton.onClick.RemoveAllListeners();
        saveButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
    }
}