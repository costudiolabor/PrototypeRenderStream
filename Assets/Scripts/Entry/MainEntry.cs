using System;
using Unity.RenderStreaming.Samples;
using System.Threading.Tasks;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform screenShotParent;
    [SerializeField] private RectTransform contentParent;

    [SerializeField] private ScreenShotView prefabScreenShotView;
    [SerializeField] private SavePanel prefabSavePanel;
    [SerializeField] private GallaryView prefabGallaryView;
    [SerializeField] private ControllerMainMenu prefabMainMenu;
    [SerializeField] private CustomImageEditor prefabCustomImageEditor;
    [SerializeField] private ViewRenderStream prefabViewRenderStream;
    [SerializeField] private ControllerRenderStream prefabControllerRenderStream;

    private ScreenShotView _screenShotView;
    private SavePanel _savePanel;
    private GallaryView _gallaryView;
    private ControllerMainMenu _mainMenu;
    private CustomImageEditor _customImageEditor;
    private ViewRenderStream _viewRenderStream;
    private ControllerRenderStream _controllerRenderStream;


    private void Awake()
    {
        _screenShotView = Instantiate(prefabScreenShotView, screenShotParent);
        _gallaryView = Instantiate(prefabGallaryView, contentParent);
        _customImageEditor = Instantiate(prefabCustomImageEditor, screenShotParent);
        _savePanel = Instantiate(prefabSavePanel, contentParent);
        _mainMenu = Instantiate(prefabMainMenu, contentParent);
        _viewRenderStream = Instantiate(prefabViewRenderStream, contentParent);
        _controllerRenderStream = Instantiate(prefabControllerRenderStream);

        _screenShotView.EndScreenShotEvent += EndScreenShot;
        _screenShotView.EndSaveScreenShotEvent += EndSaveScreenShot;
        _customImageEditor.PointerDownEvent += StartScreenShot;
        
        _mainMenu.ChangeColorEvent += ChangeColor;
        _mainMenu.MicOnEvent += MicOn; 
        _mainMenu.MicOffEvent += MicOff;  
        _mainMenu.CameraOnEvent += CameraOn;
        _mainMenu.CameraOffEvent += CameraOff; 
        
        _savePanel.UndoButtonEvent += _customImageEditor.Undo;
        _savePanel.CancelButtonEvent += _screenShotView.DisableScreenShot;
        _savePanel.CancelButtonEvent += _customImageEditor.Clear;
        _savePanel.SaveButtonEvent += SaveScreenShot;

        ActiveRemoteVideoEvent += _viewRenderStream.ActiveRemoteVideo;

        _screenShotView.Init(_controllerRenderStream.GetStreamSize());
        _controllerRenderStream.Init(_viewRenderStream);
        _mainMenu.Init(mainCamera);
        _customImageEditor.Init(screenShotParent);
        _viewRenderStream.Init();
    }

    public event Action<bool> ActiveRemoteVideoEvent;
    
    private void MicOn()
    {
        _controllerRenderStream.EnabledAudioStreamReceiver(true);
    }

    private void MicOff()
    {
        _controllerRenderStream.EnabledAudioStreamReceiver(false);
        Debug.Log("OffMIc");
    }

    private void CameraOn()
    {
        _controllerRenderStream.EnabledVideoStreamReceiver(true);
        ActiveRemoteVideoEvent?.Invoke(true);
    }

    private void CameraOff()
    {
        _controllerRenderStream.EnabledVideoStreamReceiver(false);
        ActiveRemoteVideoEvent?.Invoke(false);
    }
    
    private void ChangeColor(Color color)
    {
        _customImageEditor.SetColor(color);
    }

    private async void SaveScreenShot()
    {
        contentParent.gameObject.SetActive(false);

        Texture texture = await _screenShotView.SaveScreenShot();
        _gallaryView.AddScreenShotImage(texture);
        _screenShotView.DisableScreenShot();
        _customImageEditor.Clear();
    }


    private void StartScreenShot()
    {
        _gallaryView.Close();
        contentParent.gameObject.SetActive(false);
        _screenShotView.Take();
    }

    private void EndSaveScreenShot()
    {
        contentParent.gameObject.SetActive(true);
        _gallaryView.Open();
    }

    private void EndScreenShot()
    {
        contentParent.gameObject.SetActive(true);
        _savePanel.Open();
    }

    private void OnDestroy()
    {
        _customImageEditor.PointerDownEvent -= StartScreenShot;
        _screenShotView.EndScreenShotEvent -= EndScreenShot;
        _screenShotView.EndSaveScreenShotEvent -= EndSaveScreenShot;
        
        _mainMenu.MicOnEvent -= MicOn; 
        _mainMenu.MicOffEvent -= MicOff;  
        _mainMenu.CameraOnEvent -= CameraOn;
        _mainMenu.CameraOffEvent -= CameraOff; 
        _mainMenu.ChangeColorEvent -= ChangeColor;
        
        _savePanel.UndoButtonEvent -= _customImageEditor.Undo;
        _savePanel.CancelButtonEvent -= _screenShotView.DisableScreenShot;
        _savePanel.CancelButtonEvent -= _customImageEditor.Clear;
        _savePanel.SaveButtonEvent -= SaveScreenShot;

        ActiveRemoteVideoEvent -= _viewRenderStream.ActiveRemoteVideo;
        
    }
}