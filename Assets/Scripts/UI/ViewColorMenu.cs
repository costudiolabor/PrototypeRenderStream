using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewColorMenu : ViewBase
{
    [SerializeField] private List<ColorToggle> colorToggles;
    public event Action<Color> ChangeColorEvent;
    
    private void Awake()
    {
        foreach (var colorToggle in colorToggles)
        {
            colorToggle.ChangeColorEvent += ChangeColorEvent;
        }
    }
}