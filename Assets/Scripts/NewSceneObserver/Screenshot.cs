using UnityEngine;
using Cysharp.Threading.Tasks;

public class Screenshot : MonoBehaviour {
    public async UniTask<Texture2D> Take(){
        var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        await UniTask.WaitForEndOfFrame(this);

        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        return screenShot;
    }
    
    public async UniTask<Texture> TakeScreenShotCapture(){
         await UniTask.WaitForEndOfFrame(this);
       return ScreenCapture.CaptureScreenshotAsTexture();
    }
}
