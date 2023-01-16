using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InputManager
{
    public Control control;
    public event Action<Vector2> StartTouchEvent;
    public event Action CancelTouchEvent;
    public event Action<Vector2> DragTouchEvent;

    public void Init()
    {
        control = new Control();

        control.Touch.TouchPress.started += ctx => StartTouch(control.Touch.Drag.ReadValue<Vector2>());
        control.Touch.TouchPress.canceled += ctx => CancelTouch();
        control.Touch.Drag.performed += Drag;
    }

    public void Drag(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        DragTouchEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void StartTouch(Vector2 value)
    {
        Debug.Log("Start " + value);
        StartTouchEvent?.Invoke(value);
    }

    public void CancelTouch()
    {
        Debug.Log("Cancel");
        CancelTouchEvent?.Invoke();
    }

    public void OnEnable()
    {
        control.Enable();
    }

    public void OnDisable()
    {
        control.Disable();
    }
}
