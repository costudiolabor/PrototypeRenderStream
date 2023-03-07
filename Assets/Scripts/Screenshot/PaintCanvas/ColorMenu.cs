using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorMenu : HidePanel {
   [SerializeField] private ColorPointer[] colors;
   [SerializeField] private Image sampleImage;

   public event Action<Color> ColorChangedEvent;

   private void OnColorChanged(Color color){
      sampleImage.color = color;
      ColorChangedEvent?.Invoke(color);
      Close();
   }
   private void OnEnable(){
      foreach (var item in colors)
         item.OnColorSampledEvent += OnColorChanged;
   }

   private void OnDisable(){
      foreach (var item in colors)
         item.OnColorSampledEvent -= OnColorChanged;
   }
}
