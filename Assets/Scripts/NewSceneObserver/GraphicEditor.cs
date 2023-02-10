using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GraphicEditor : ViewOperator<GraphicEditorView>, IDisposable {
    [SerializeField] private Editors editors;
    
    private Color _color;
    protected float width;
    protected UniTaskCompletionSource<Texture2D> taskCompletionSource;

    public void Initialize(){
        base.CreateView();
        SubscribeUIEvent();

        editors.Initialize(view.drawingCanvas.instaceParent);

       SubscribeDragEvents();
       SubscribeTapEvents();

    }

    public async UniTask<Texture2D> EditProcess(Texture2D textureForEdit){
        editors.Clear();
        view.Open();
        view.drawingCanvas.backgroundTexture = textureForEdit;
        taskCompletionSource = new UniTaskCompletionSource<Texture2D>();
        var resultTexture = await taskCompletionSource.Task;
        view.Close();
        return resultTexture;
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

    private async void OnAccept(){
        //view.tools.Close();
        var texture = await editors.ScreenShotTake();
       // view.tools.Open();
        taskCompletionSource.TrySetResult(texture);
    }

    private void OnReject(){
        taskCompletionSource.TrySetCanceled();
        view.Close();
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
    }

    private void UnsubscribeUIEvents(){
        view.ArrowSelectedEvent -= editors.SelectArrow;
        view.LineSelectedEvent -= editors.LineSelect;
        view.StickerSelectedEvent -= OnStickerSelect;

        view.AcceptClickedEvent -= OnAccept;
        view.UndoEvent -= editors.Undo;
        view.RejectClickedEvent -= OnReject;
        view.colorMenu.ColorChangedEvent -= SetColor;
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