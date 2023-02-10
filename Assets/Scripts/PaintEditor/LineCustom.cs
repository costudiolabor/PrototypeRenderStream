using System;
using UnityEngine;

[Serializable]
public class LineCustom : Figure
{
    [SerializeField] private Material materialLine;
    [SerializeField] private float deltaPosition = 0.1f;

    private int _minCountPoint = 3;
    private LineRenderer _lineRenderer;
    private Vector2 _lastPosition;
    private float _currentDeltaPosition;

    public override void CreateGameObject(Vector3 position)
    {
        //Debug.Log("LineCustom");
        _lineRenderer = new GameObject("LineCustom").AddComponent<LineRenderer>();
        _lineRenderer.material = materialLine;
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
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
