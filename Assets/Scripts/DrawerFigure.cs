using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class DrawerFigure
{
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private float _offSetZ = 0.2f;

    private bool _isHitRayCast;
    private Figure currentFigure;
    private Stack<Figure> figures = new Stack<Figure>();

    public DrawerFigure(Camera _cameraMain)
    {
        this._cameraMain = _cameraMain;
    }

    public void StartTouch(Vector2 touchPosition)
    {
        Vector3 position = GetPositionPlace(touchPosition);
        if (!_isHitRayCast) return;
        currentFigure = new Line(position);
        figures.Push(currentFigure);
    }

    public void CancelTouch()
    {
        if (figures.Count > 0)
        {
            Figure figure = figures.Peek();

            if (!figure.EndDraw())
            {
                figure = null;
            }
            currentFigure = null;
        }
    }

    public void Drag(Vector2 touchPosition)
    {
        if (currentFigure != null)
        {
            Vector3 position = GetPositionPlace(touchPosition);
            currentFigure.Draw(position);
        }
    }

    public void DeleteFigure()
    {
        Debug.Log("del");
        if (figures.Count > 0)
        {
            Figure figure = figures.Pop();
            figure.DestroyObject();
            figure = null;
        }
    }

    public Vector3 GetPositionPlace(Vector2 touchPosition)
    {
        RaycastHit hit = RayFromCamera(touchPosition, 1000.0f);
        return new Vector3(hit.point.x, hit.point.y, hit.point.z - _offSetZ);
    }

    public RaycastHit RayFromCamera(Vector3 touchPosition, float rayLength)
    {
        RaycastHit hit;
        Ray ray = _cameraMain.ScreenPointToRay(touchPosition);
        _isHitRayCast = Physics.Raycast(ray, out hit, rayLength);
        return hit;
    }

}
