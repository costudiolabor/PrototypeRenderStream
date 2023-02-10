using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GraphicEditor : ViewOperator<GraphicEditorView>, IDisposable
{
    [SerializeField] private ImageEditorModel imageEditorModel;
    [SerializeField] private StickerModel stickerModel;
    private Color _color;
    protected float width;
    protected UniTaskCompletionSource<Texture2D> taskCompletionSource;

    public override void InitializeView() {
        base.InitializeView();
        view.tools.AcceptClickedEvent += OnAccept;
        view.tools.RejectClickedEvent += OnReject;
        view.tools.UndoEvent += imageEditorModel.Undo;

        //SubscriptionDragDrop();
        
        //view.tools.ArrowSelectedEvent += OnArrowSelect;
       // view.tools.LineSelectedEvent += OnLineSelect;
       // view.tools.StickerSelectedEvent += OnStickerSelect;
        
       

        view.tools.colorMenu.ColorChangedEvent += SetColor;
        
        imageEditorModel.Initialize(view.linesParent);
        stickerModel.Initialize(view.linesParent);
    }
    
    public async void OnStart() {
        view.tools.Close();
        var texture = await imageEditorModel.ScreenShotTake();
        view.tools.Open();

        await EditProcess(texture);
        taskCompletionSource.TrySetResult(texture);
    }

    private void OnArrowSelect() {
        SubscriptionDragDrop();
        imageEditorModel.OnArrowSelect();
    }
    
    private void OnLineSelect() { 
        SubscriptionDragDrop();
        imageEditorModel.OnLineSelect();
    }
    
    private void OnStickerSelect()
    {
        UnSubscribeDragDrop();
    }

    private void SetColor(Color color) {
        stickerModel.SetColor(color);
        imageEditorModel.SetColor(color);
    }

    private void OnPointerUp(Vector2 rectPoint) {
        stickerModel.CreateSticker(rectPoint);
    }

    private void SubscriptionDragDrop() {
        view.DragEvent += imageEditorModel.Draw;
        view.DropEvent += imageEditorModel.StopDraw;
        view.PointerUpEvent -= OnPointerUp;
    }
    
    
    private void UnSubscribeDragDrop() {
        view.DragEvent -= imageEditorModel.Draw;
        view.DropEvent -= imageEditorModel.StopDraw;
        view.PointerUpEvent += OnPointerUp;
    }
    

    
    public async UniTask<Texture2D> EditProcess(Texture2D textureForEdit) {
        imageEditorModel.Clear();
        view.Open();
        view.SetBackground(textureForEdit);
        taskCompletionSource = new UniTaskCompletionSource<Texture2D>();
        var resultTexture = await taskCompletionSource.Task;
        view.Close();
        return resultTexture;
    }

    private async void OnAccept() {
        view.tools.Close();
        var texture = await imageEditorModel.ScreenShotTake();
        view.tools.Open();
        taskCompletionSource.TrySetResult(texture);
    }

    private void OnReject() {
        taskCompletionSource.TrySetCanceled();
        view.Close();
    }

    public void Dispose() {
        view.tools.AcceptClickedEvent -= OnAccept;
        view.tools.RejectClickedEvent -= OnReject;
        view.DragEvent -= imageEditorModel.Draw;
        view.DropEvent -= imageEditorModel.StopDraw;
        view.tools.UndoEvent -= imageEditorModel.Undo;
        view.tools.ArrowSelectedEvent -= imageEditorModel.OnArrowSelect;
        view.tools.LineSelectedEvent -= imageEditorModel.OnLineSelect;
        view.tools.colorMenu.ColorChangedEvent -= imageEditorModel.SetColor;
    }
}