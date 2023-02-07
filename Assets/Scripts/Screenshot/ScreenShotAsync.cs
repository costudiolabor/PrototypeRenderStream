using UnityEngine;
using Cysharp.Threading.Tasks;

public class ScreenShotAsync : MonoBehaviour {
    public async UniTask<Texture2D> Take(Vector2Int screenSize){
        var screenShot = new Texture2D(screenSize.x, screenSize.y, TextureFormat.RGB24, false);
        //var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        await UniTask.WaitForEndOfFrame(this);

        screenShot.ReadPixels(new Rect(0, 0, screenSize.x, screenSize.y), 0, 0);
        //screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        return screenShot;
    }
}
