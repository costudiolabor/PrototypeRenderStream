using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ImageEditorModel
{
    [SerializeField] protected LineFactory lineFactory;
    [SerializeField] protected Screenshot screenshotPrefab;
    protected Screenshot _screenshot;
    protected Stack<GameObject> _undoStack = new Stack<GameObject>();
    protected Drawer _drawer = new Drawer();
    protected Color _color = Color.red;
    protected BrushType _brushType;
    protected float _brushSize = 25f;

    public virtual void Initialize(RectTransform linesParent)
    {
        if (!_screenshot) _screenshot = Object.Instantiate(screenshotPrefab);
        lineFactory.SetParent(linesParent);
    }

    public UniTask<Texture2D> ScreenShotTake()
    {
        return _screenshot.Take();
    }

    public virtual void Draw(Vector2 rectPoint)
    {
        if (_drawer.line == null)
        {
            _drawer.line = lineFactory.GetBrush(_brushType);
            _drawer.line.Initialize(_color, _brushSize);
        }

        _drawer.Draw(rectPoint);
    }

    public virtual void StopDraw()
    {
        if (_drawer.line.PositionsCount > 1)
            _undoStack.Push(_drawer.line.gameObject);
        _drawer.StopDraw();
    }

    public void Undo()
    {
        if (_undoStack.Count == 0) return;
        GameObject currentGameObject = _undoStack.Pop();
        Object.Destroy(currentGameObject);
    }

    public void Clear()
    {
        foreach (var line in _undoStack)
            Object.Destroy(line.gameObject);
        _undoStack.Clear();
    }


    public void SetColor(Color color) =>
        _color = color;

    public void OnArrowSelect() =>
        _brushType = BrushType.Arrow;


    public void OnLineSelect() =>
        _brushType = BrushType.Line;
}