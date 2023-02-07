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

  public void SetRawImage(Texture texture)
  {
    _rawImage.texture = texture;
  }
  
}
