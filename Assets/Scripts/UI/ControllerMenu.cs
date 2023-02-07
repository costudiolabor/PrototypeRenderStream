using System;
using UnityEngine;

public class ControllerMenu : MonoBehaviour
{
    [SerializeField] private ViewMenu _viewMenu;

    public event Action<Color> ChangeColorEvent;
    public event Action
        MicOnEvent,
        MicOffEvent,
        CameraOnEvent,
        CameraOffEvent;

    public void Init()
    {
        _viewMenu.MicOnEvent += () => MicOnEvent?.Invoke(); 
        _viewMenu.MicOffEvent += () => MicOffEvent?.Invoke();  
        _viewMenu.CameraOnEvent += () => CameraOnEvent?.Invoke();
        _viewMenu.CameraOffEvent += () =>  CameraOffEvent?.Invoke(); 
        _viewMenu.ChangeColorEvent += color => ChangeColorEvent?.Invoke(color);
    }
    
    private void OnDestroy()
    {
        MicOnEvent = null;
        MicOffEvent = null;
        CameraOnEvent = null;
        CameraOffEvent = null;
        ChangeColorEvent = null;
       
    }

   
}