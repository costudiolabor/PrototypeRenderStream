using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenShotCoroutine : MonoBehaviour
{
    private Texture2D _renderResult;

    private Coroutine _coroutine;

    public async Task<Texture2D> Take()
    {
        _coroutine = StartCoroutine(GetScreenShot());
        
        while (_coroutine != null)
        {
            await Task.Yield();
        }

        return _renderResult;
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
