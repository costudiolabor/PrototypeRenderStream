using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class DrawerFigure
{
    [SerializeField] private float offSetZ = 0.2f;

    [SerializeField] private Camera _cameraMain;
    private bool _isHitRayCast;
    private Figure _currentFigure;
    private Stack<Figure> _figures = new Stack<Figure>();

    public void Init(Camera cameraMain)
    {
        Debug.Log("DrawerFigure");
        _cameraMain = cameraMain;
    }

    public void StartTouch(Vector2 touchPosition)
    {
        var position = GetPositionPlace(touchPosition);
        if (!_isHitRayCast) return;
        _currentFigure = new Line();
        _currentFigure.CreateGameObject(position);
        
        _figures.Push(_currentFigure);
    }

    public void CancelTouch()
    {
        if (_figures.Count <= 0) return;
        var figure = _figures.Peek();

        if (figure.EndDraw() == false)
        {
            _figures.Pop();
        }
        _currentFigure = null;
    }

    public void Drag(Vector2 touchPosition)
    {
        if (_currentFigure == null) return;
        var position = GetPositionPlace(touchPosition);
        _currentFigure.Draw(position);
    }

    public void DeleteFigure()
    {
        Debug.Log("del");
        if (_figures.Count <= 0) return;
        var figure = _figures.Pop();
        figure.DestroyObject();
    }

    public Vector3 GetPositionPlace(Vector2 touchPosition)
    {
        var hit = RayFromCamera(touchPosition, 1000.0f);
        return new Vector3(hit.point.x, hit.point.y, hit.point.z - offSetZ);
    }

    public RaycastHit RayFromCamera(Vector3 touchPosition, float rayLength)
    {
        var ray = _cameraMain.ScreenPointToRay(touchPosition);
        _isHitRayCast = Physics.Raycast(ray, out var hit, rayLength);
        return hit;
    }

}
