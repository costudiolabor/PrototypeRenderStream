using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    //[SerializeField] private Streamer streamer = new Streamer();
    [SerializeField] private GraphicEditor graphicEditor;

    private async void Awake() {
        //streamer.Initialize();
        graphicEditor.Initialize();
        await graphicEditor.EditProcess(new Texture2D(1,1));
    }
}