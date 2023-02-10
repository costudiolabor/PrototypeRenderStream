using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ImageEditor : ViewOperator<ImageEditorView>, IDisposable
{
    [SerializeField] protected ImageEditorModel imageEditorModel; 
    protected float width;
    protected UniTaskCompletionSource<Texture2D> taskCompletionSource;

    public  void InitializeView() {
        base.InitializeView();
        view.tools.AcceptClickedEvent += OnAccept;
        view.tools.RejectClickedEvent += OnReject;
        view.DragEvent += imageEditorModel.Draw;
        view.DropEvent += imageEditorModel.StopDraw;
        view.tools.UndoEvent += imageEditorModel.Undo;
        view.tools.ArrowSelectedEvent += imageEditorModel.OnArrowSelect;
        view.tools.LineSelectedEvent += imageEditorModel.OnLineSelect;
     
        view.tools.colorMenu.ColorChangedEvent += imageEditorModel.SetColor;
        imageEditorModel.Initialize(view.linesParent);
    }

    public async void OnStart()
    {
        view.tools.Close();
        var texture = await imageEditorModel.ScreenShotTake();
        view.tools.Open();
    
        await EditProcess(texture);
        taskCompletionSource.TrySetResult(texture);
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

    private async void OnAccept(){
        view.tools.Close();
        var texture = await imageEditorModel.ScreenShotTake();
        view.tools.Open();
        taskCompletionSource.TrySetResult(texture);
    }
    
    private void OnReject() {
        taskCompletionSource.TrySetCanceled();
        view.Close();
    }

    public void Dispose()
    {
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