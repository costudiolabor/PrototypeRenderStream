using Enums;
using System;
using UnityEngine;
using System.Collections.Generic;



public class CustomImageEditor : MonoBehaviour
{
    [SerializeField] private LineFactory lineFactory;
    [SerializeField] private float brushSize = 25f;
    [SerializeField] private CustomImageEditorView customImageEditorView;
    
    private readonly Stack<Line> _undoStack = new();
    private readonly Drawer _drawer = new();
    
    private RectTransform _linesParent;
    private Color _color = Color.red;
    private BrushType _brushType;
    
    public event Action PointerDownEvent;
    public void Init(RectTransform linesParent)
    {
        _linesParent = linesParent;
        customImageEditorView.Init();
        customImageEditorView.DragEvent += Draw;
        customImageEditorView.DropEvent += StopDraw;
        customImageEditorView.PointerDownEvent += delegate { PointerDownEvent?.Invoke(); }; ;
        lineFactory.SetParent(linesParent);
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
    
    public void Undo(){
        if (_undoStack.Count == 0) return;
        var line = _undoStack.Pop();
        Destroy(line.gameObject);
    }

    public void SetColor(Color color) =>
        _color = color;

    public void Clear()
    {
        while (_undoStack.Count != 0) Undo();
    }
}
