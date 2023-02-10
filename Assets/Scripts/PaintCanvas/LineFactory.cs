using System;
using Enums;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class LineFactory {
    [SerializeField] private Line linePrefab;
    [SerializeField] private Arrow arrowPrefab;
   // [SerializeField] private GameObject stickerPrefab;
    private RectTransform _parent;

    public void SetParent(RectTransform linesParentTransform){
        _parent = linesParentTransform;
    }

    public Line GetBrush(BrushType type){
        return type switch
        {
            (BrushType.Line) => Get(linePrefab),
            (BrushType.Arrow) => Get(arrowPrefab),
            //(BrushType.Sticker) => Get(labelPrefab),
            _ => Get(linePrefab)
        };
    }

    private T Get<T>(T prefab) where T : Line{
        return Object.Instantiate(prefab, _parent);
    }
}