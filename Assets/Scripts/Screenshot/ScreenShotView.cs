using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.RenderStreaming;
using UnityEngine;
using UnityEngine.UI;


public class ScreenShotView : ViewBase
{
    [SerializeField] private RawImage paintImage;
    [SerializeField] private ScreenShotAsync screenShotAsync;

    private Texture2D _renderResult;
    private Coroutine _coroutine;
    private Vector2Int _streamSize;

    public event Action EndScreenShotEvent;
    public event Action EndSaveScreenShotEvent;
    
    

    public void Init(Vector2Int streamSize)
    {
        _streamSize = streamSize;
        paintImage.gameObject.SetActive(false);
    }

    public async void Take()
    {
        if (paintImage.texture == null)
        {
            Texture texture = await screenShotAsync.Take(_streamSize); 
            SetPaintImage(texture);
        }

        EndScreenShotEvent?.Invoke();
    }

    public async Task<Texture> SaveScreenShot()
    {
        Texture texture = await screenShotAsync.Take(_streamSize); 
        EndSaveScreenShotEvent?.Invoke();
        return texture;
    }

    public void DisableScreenShot()
    {
        paintImage.gameObject.SetActive(false);
        paintImage.texture = null;
    }

    private void SetPaintImage(Texture renderResult)
    {
        paintImage.gameObject.SetActive(true);
        paintImage.texture = renderResult;
    }
}