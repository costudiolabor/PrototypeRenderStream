using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class ScreenShotView : ViewBase
{
    [SerializeField] private RawImage paintImage;

    private Texture2D _renderResult;
    private Coroutine _coroutine;

    public event Action EndScreenShotEvent;
    public event Action EndSaveScreenShotEvent;
    

    public void Init()
    {
        paintImage.gameObject.SetActive(false);
    }

    public async void Take()
    {
        if (paintImage.texture == null)
        {
            Texture texture = await AsyncTake();
            SetPaintImage(texture);
        }

        EndScreenShotEvent?.Invoke();
    }

    public async Task<Texture2D> SaveScreenShot()
    {
         Texture2D texture = await AsyncTake();
         EndSaveScreenShotEvent?.Invoke();
         return texture;
    }

    private async Task<Texture2D> AsyncTake()
    {
        _coroutine = StartCoroutine(GetScreenShot());
        while (_coroutine != null)
        {
            await Task.Yield();
        }

        return _renderResult;
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

    private IEnumerator GetScreenShot()
    {
        int width = Screen.width;
        int height = Screen.height;
        yield return new WaitForEndOfFrame();
        _renderResult = new Texture2D(width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, width, height);
        _renderResult.ReadPixels(rect, 0, 0);
        _renderResult.Apply();
        _coroutine = null;
    }
}