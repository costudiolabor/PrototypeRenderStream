using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotCamera : MonoBehaviour
{
    // public Camera captureCamera;
    //
    // [SerializeField] private RawImage rawImage;
    // [SerializeField] private RawImage rawImageScreen;
    // [SerializeField] Transform transform;
    // [SerializeField] GameObject hidePanel;
    //
    // Coroutine coroutine;
    // Coroutine coroutineScreen;
    //
    // Texture2D tex;
    //
    // private void Update()
    // {
    //     transform.Rotate(10, 15, 12);
    // }
    //
    // public async void GetScreenShotCamera()
    // {
    //     coroutine = StartCoroutine(TakeScreenShotCamera());
    //     while (coroutine != null)
    //     {
    //         await Task.Yield();
    //     }
    //     rawImage.texture = tex;
    // }
    //
    // public async void GetScreenShot()
    // {
    //     coroutine = StartCoroutine(TakeScreenShot());
    //     while (coroutineScreen != null)
    //     {
    //         await Task.Yield();
    //     }
    //     rawImageScreen.texture = tex;
    // }
    //
    //
    // IEnumerator TakeScreenShotCamera()
    // {
    //     int width = this.captureCamera.pixelWidth;
    //     int height = this.captureCamera.pixelHeight;
    //     tex = new Texture2D(width, height);
    //     RenderTexture targetTexture = RenderTexture.GetTemporary(width, height);
    //     yield return new WaitForEndOfFrame();
    //     captureCamera.targetTexture = targetTexture;
    //     captureCamera.Render();
    //     RenderTexture.active = targetTexture;
    //     Rect rect = new Rect(0, 0, width, height);
    //     tex.ReadPixels(rect, 0, 0);
    //     tex.Apply();
    //     targetTexture = null;
    //     RenderTexture.active = null; // JC: added to avoid errors
    //     captureCamera.targetTexture = null;
    //     Destroy(targetTexture);
    //     coroutine = null;
    //
    //     // // Create a texture the size of the screen, RGB24 format
    //     // int width = Screen.width;
    //     // int height = Screen.height;
    //     // //hidePanel.SetActive(false);
    //     // yield return new WaitForEndOfFrame();
    //     // tex = new Texture2D(width, height, TextureFormat.RGB24, true);
    //     // // Read screen contents into the texture
    //     // tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //     // tex.Apply();
    //     //// hidePanel.SetActive(true);
    //     // coroutine = null;
    // }
    //
    // IEnumerator TakeScreenShot()
    // {
    //     // Create a texture the size of the screen, RGB24 format
    //     int width = Screen.width;
    //     int height = Screen.height;
    //     //hidePanel.SetActive(false);
    //     yield return new WaitForEndOfFrame();
    //     tex = new Texture2D(width, height, TextureFormat.RGB24, true);
    //     // Read screen contents into the texture
    //     tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //     tex.Apply();
    //     // hidePanel.SetActive(true);
    //     coroutineScreen = null;
    // }



}
