using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class View : MonoBehaviour
{
    private InputManager _inputManager;

    [SerializeField] private Button buttonDelete;
    public event Action ClickButtonDeleteEvent;

    public void Awake()
    {
        buttonDelete.onClick.AddListener(() => ClickButtonDeleteEvent?.Invoke());
    }
}