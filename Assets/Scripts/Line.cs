using System;
using UnityEngine;

[Serializable]
public class Line : Figure
{
    [SerializeField] private Material _materialLine;
    [SerializeField] private float _deltaPosition = 3.1f;

    private int minCountPoint = 4;

    private LineRenderer _lineRenderer;
    private Vector2 _lastPosition;
    private float _currentdeltaPosition;

    public Line(Vector3 position)
    {
        CreateGameObject(position);
    }

    public void CreateGameObject(Vector3 position)
    {
        _lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        _lineRenderer.material = _materialLine;
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
        _currentdeltaPosition = Vector3.Distance(_lastPosition, position);

        if (_currentdeltaPosition > _deltaPosition)
        {
            _lastPosition = position;
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        }
    }

    public override bool EndDraw()
    {
        if (_lineRenderer.positionCount < minCountPoint)
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
        UnityEngine.Object.Destroy(_lineRenderer.gameObject);
    }

}
