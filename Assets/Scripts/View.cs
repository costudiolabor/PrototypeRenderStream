using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class View : MonoBehaviour
{
    private InputManager _inputManager;

    [SerializeField] private Button _buttonDelete;
    public event Action ClickButtonDeleteEvent;

    public void Awake()
    {
        if (_buttonDelete) _buttonDelete.onClick.AddListener(() => ClickButtonDeleteEvent?.Invoke());  
    }
}
