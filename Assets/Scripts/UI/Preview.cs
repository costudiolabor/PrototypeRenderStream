using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview : ViewBase
{
  [SerializeField] private RawImage _rawImage;
  [SerializeField] private Button buttonClose;

  private void Awake()
  {
    buttonClose.onClick.AddListener(Close);
  }

  public void SetRawImage(Texture2D texture)
  {
    _rawImage.texture = texture;
  }
  
}
