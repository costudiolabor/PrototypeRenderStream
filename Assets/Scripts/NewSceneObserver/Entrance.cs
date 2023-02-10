using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    //[SerializeField] private Streamer streamer = new Streamer();
    [SerializeField] private GraphicEditor graphicEditor;

    private void Awake() {
        //streamer.Initialize();
        graphicEditor.InitializeView();
        graphicEditor.OnStart();
    }
}