using System;
using Enums;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class LineFactory {
    [SerializeField] private LineCanvas linePrefab;
   // [SerializeField] private Arrow arrowPrefab;
    private RectTransform _parent;

    public void Initialize(RectTransform linesParentTransform){
        _parent = linesParentTransform;
    }

    public LineCanvas GetBrush(BrushType type){
        //Debug.Log(type);
        return type switch
        {
            (BrushType.Line) => Get(linePrefab),
            //(BrushType.Arrow) => Get(arrowPrefab),
            _ => Get(linePrefab)
        };
    }

    private T Get<T>(T prefab) where T : LineCanvas{
        return Object.Instantiate(prefab, _parent);
    }
}