using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugKeyBoard : MonoBehaviour
{
    [SerializeField] private Keyboard _keyboard;
    void Awake() {
        Debug.Log("start");

        _keyboard = new Keyboard();
        _keyboard.onTextInput += PrintChar;
    }

    private void PrintChar(char c)
    {
        Debug.Log(c.ToString());
    }
}
