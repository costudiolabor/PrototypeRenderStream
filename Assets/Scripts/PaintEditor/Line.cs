using System;
using UnityEngine;

[Serializable]
public class Line : Figure
{
    [SerializeField] private Material materialLine;
    [SerializeField] private float deltaPosition = 3.1f;

    private int _minCountPoint = 4;
    private LineRenderer _lineRenderer;
    private Vector2 _lastPosition;
    private float _currentDeltaPosition;

    public override void CreateGameObject(Vector3 position)
    {
        //Debug.Log("Line");
        _lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        _lineRenderer.material = materialLine;
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.numCapVertices = 50;
        _lineRenderer.SetPosition(0, position);
        _lineRenderer.SetPosition(1, position);
        _lastPosition = position;
    }

    public override void Draw(Vector3 position)
    {
        _currentDeltaPosition = Vector3.Distance(_lastPosition, position);

        if (!(_currentDeltaPosition > deltaPosition)) return;
        _lastPosition = position;
        var positionCount = _lineRenderer.positionCount;
        positionCount++;
        _lineRenderer.positionCount = positionCount;
        _lineRenderer.SetPosition(positionCount - 1, position);
    }

    public override bool EndDraw()
    {
        if (_lineRenderer.positionCount < _minCountPoint)
        {
            DestroyObject();
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void DestroyObject()
    {
        if (!_lineRenderer.gameObject) return;
        UnityEngine.Object.Destroy(_lineRenderer.gameObject);
    }

}
