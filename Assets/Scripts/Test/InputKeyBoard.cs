using System;
using UnityEngine.InputSystem;
using Unity.RenderStreaming;
using UnityEngine;

public class InputKeyBoard
{
    private InputReceiver _inputReceiver;
    private Keyboard _keyboard;

    public event Action<char> CharInputEvent; 

    public void Initialize(InputReceiver inputReceiver) {
        _inputReceiver = inputReceiver;
        _inputReceiver.onDeviceChange += OnDeviceChange;
    }

    private  void OnDeviceChange(InputDevice device, InputDeviceChange change) {
        switch (change) {
            case InputDeviceChange.Added: {
                _inputReceiver.PerformPairingWithDevice(device);
                SetDevice(device, true);
                return;
            }
            case InputDeviceChange.Removed: {
                _inputReceiver.UnpairDevices(device);
                SetDevice(device, false);
                return;
            }
        }
    }

    private  void SetDevice(InputDevice device, bool add = false) {
        switch (device) {
            case Keyboard keyboard:
                _keyboard = add ? keyboard : null;
                if (add)
                    _keyboard.onTextInput += OnCharInput;
                return;
        }
    }
    
    private void OnCharInput(char charInput) {
        CharInputEvent?.Invoke(charInput);
    }
}