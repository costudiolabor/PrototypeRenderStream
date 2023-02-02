using System.Collections.Generic;
using Enums;
//using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;

[System.Serializable]
public class ImageEditor : ViewOperator<ImageEditorView> 
{
    [SerializeField] private float brushSize = 25f;
    [SerializeField] private LineFactory lineFactory;
    [SerializeField] private ScreenShotView screenshotPrefab;
    
    private readonly Stack<LineCanvas> _undoStack = new();
    private readonly Drawer _drawer = new();

    private  ScreenShotView _screenshot;
    private Color _color = Color.red;
    private BrushType _brushType;
    private float _width;
    //private UniTaskCompletionSource<Texture2D> _taskCompletionSource;

   
        
    public override void InitializeView(){
        base.InitializeView();
        view.tools.AcceptClickedEvent += OnAccept;
        view.tools.RejectClickedEvent += OnReject;
        view.DragEvent += Draw;
        view.DropEvent += StopDraw;
        view.tools.UndoEvent += Undo;
        view.tools.ArrowSelectedEvent += OnArrowSelect;
        view.tools.LineSelectedEvent += OnLineSelect;
        view.tools.colorMenu.ColorChangedEvent += SetColor;

        view.tools.ScreenShotEvent += EditProcess;
        
        //_screenshot = Object.Instantiate(screenshotPrefab);
    }

    public void EditProcess()
    {
        Debug.Log("Screen");
        Texture2D textureForEdit = new Texture2D(100,100);
        Clear();
        view.Open();
        view.SetTexture(textureForEdit);
        lineFactory.Initialize(view.linesParent);
    
        var resultTexture = new Texture2D(100,100);
        view.Close();
    
        //return resultTexture;
    }
    
    // public async UniTask<Texture2D> EditProcess(Texture2D textureForEdit){
    //     Clear();
    //     view.Open();
    //     view.SetTexture(textureForEdit);
    //     lineFactory.Initialize(view.linesParent);
    //
    //     _taskCompletionSource = new UniTaskCompletionSource<Texture2D>();
    //      var resultTexture = await _taskCompletionSource.Task;
    //     view.Close();
    //
    //     return resultTexture;
    // }
    
    private async void OnAccept(){
        view.tools.Close();
        //var texture = await _screenshot.Take();
        view.tools.Open();
        //_taskCompletionSource.TrySetResult(texture);
    }

    private void Draw(Vector2 rectPoint){
        if (_drawer.line == null){
            _drawer.line = lineFactory.GetBrush(_brushType);
            _drawer.line.Initialize(_color, brushSize);
        }

        _drawer.Draw(rectPoint);
    }

    private void StopDraw(){
        if (_drawer.line.PositionsCount > 1)
            _undoStack.Push(_drawer.line);

        _drawer.StopDraw();
    }

    private void Undo(){
        if (_undoStack.Count == 0) return;

        var line = _undoStack.Pop();
        Object.Destroy(line.gameObject);
    }

    private void OnReject(){
       // _taskCompletionSource.TrySetCanceled();
        view.Close();
    }

    private void Clear(){
        foreach (var line in _undoStack)
            Object.Destroy(line.gameObject);

        _undoStack.Clear();
    }

    private void SetColor(Color color) =>
        _color = color;

    private void OnArrowSelect()
    {
        
        Debug.Log("Arrow");
        _brushType = BrushType.Arrow;
    }
    
    private void OnLineSelect() =>
        _brushType = BrushType.Line;

    private void OnEnable(){

    }


}