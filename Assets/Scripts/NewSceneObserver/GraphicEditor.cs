using System;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GraphicEditor : ViewOperator<GraphicEditorView>, IDisposable {
    [SerializeField] private Editors editors;
    [SerializeField] private Screenshot screenshotPrefab;

    private  Screenshot _screenshot;
    //private Vector2Int _screenSize;
    private Color _color;
    protected float width;
    protected UniTaskCompletionSource<Texture> _taskCompletionSource;
    
    public event Action<Texture> SaveScreenShotEvent;
    public event Action CloseViewEvent, OpenViewEvent;

    public void Initialize(){
        base.CreateView();
        SubscribeUIEvent();
        editors.Initialize(view.drawingCanvas.instaceParent);
        SubscribeDragEvents();
        SubscribeTapEvents();
        _screenshot = Object.Instantiate(screenshotPrefab);
    }

    public async void OnStart()
    {
        view.tools.gameObject.SetActive(false); 
        var texture = await _screenshot.TakeScreenShotCapture();
        view.tools.gameObject.SetActive(true);
        await EditProcess(texture);
        _taskCompletionSource.TrySetResult(texture);
    }

    
    public async UniTask<Texture> EditProcess(Texture textureForEdit){
        editors.Clear();
        view.Open();
        view.drawingCanvas.backgroundTexture = textureForEdit;
        _taskCompletionSource = new UniTaskCompletionSource<Texture>();
        var resultTexture = await _taskCompletionSource.Task;
        view.Close();
        return resultTexture;
    }
    
    private async void OnAccept(){
        view.tools.gameObject.SetActive(false);
        CloseViewEvent?.Invoke();
        var texture = await _screenshot.TakeScreenShotCapture();
        view.tools.gameObject.SetActive(true);
        OpenViewEvent?.Invoke();
        _taskCompletionSource.TrySetResult(texture);
        SaveScreenShotEvent?.Invoke(texture);
    }

    private void OnReject(){
        _taskCompletionSource.TrySetCanceled();
        view.Close();
    }
    
    private void OnArrowSelect(){
        editors.SelectArrow();
    }

    private void OnLineSelect(){
        editors.LineSelect();
    }

    private void OnStickerSelect(){
        editors.StickerSelect();
    }

    private void SetColor(Color color){
        editors.SetColor(color);
    }

    private void OnPointerDown(Vector2 rectPoint){
        editors.OnPointerDown(rectPoint);
    }

    private void OpenPopupDescriptionSticker(Sticker sticker) {
        view.popupDescriptionSticker.Open();
        view.popupDescriptionSticker.SetTextPopup(sticker);
    }

    public void CharInput(char charInput) {
        view.popupDescriptionSticker.SetCharInputField(charInput);
    }

    public void Dispose(){
        UnsubscribeAllTouchEvents();
        UnsubscribeUIEvents();
    }
    
    private void SubscribeUIEvent(){
        view.AcceptClickedEvent += OnAccept;
        view.RejectClickedEvent += OnReject;
        view.UndoEvent += editors.Undo;
        view.ArrowSelectedEvent += OnArrowSelect;
        view.LineSelectedEvent += OnLineSelect;
        view.StickerSelectedEvent += OnStickerSelect;
        view.colorMenu.ColorChangedEvent += SetColor;
        //editors.stickerEditor.SelectObjectEvent += OpenPopupDescriptionSticker;
    }

    private void UnsubscribeUIEvents(){
        view.ArrowSelectedEvent -= editors.SelectArrow;
        view.LineSelectedEvent -= editors.LineSelect;
        view.StickerSelectedEvent -= OnStickerSelect;
        view.AcceptClickedEvent -= OnAccept;
        view.UndoEvent -= editors.Undo;
        view.RejectClickedEvent -= OnReject;
        view.colorMenu.ColorChangedEvent -= SetColor;
       // editors.stickerEditor.SelectObjectEvent -= OpenPopupDescriptionSticker;
    }

    private void SubscribeDragEvents(){
        view.drawingCanvas.DragEvent += editors.OnDrag;
        view.drawingCanvas.DropEvent += editors.OnDrop;
    }

    private void SubscribeTapEvents(){
        view.drawingCanvas.PointerDownEvent += OnPointerDown;
    }

    private void UnsubscribeAllTouchEvents(){
        view.drawingCanvas.DragEvent -= editors.OnDrag;
        view.drawingCanvas.DropEvent -= editors.OnDrop;
        view.drawingCanvas.PointerDownEvent -= OnPointerDown;
    }
}