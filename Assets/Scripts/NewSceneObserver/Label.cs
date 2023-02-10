using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label : Line
{
    [SerializeField] private  Image image;
    public override void LineIsReady(){
          Debug.Log("Label");
          SetLabelColor();
    }

    private void SetLabelColor() {
        image.color = color;
    }
}
