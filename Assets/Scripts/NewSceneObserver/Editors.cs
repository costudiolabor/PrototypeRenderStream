using Enums;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class Editors  {
     [SerializeField] private LineEditorModel lineEditor;
     public StickerEditor stickerEditor;

     private ToolType _selectedTool;

    public void Initialize(RectTransform parent)
    {
          lineEditor.Initialize(parent);
          stickerEditor.Initialize(parent);
     }
     
     public UniTask<Texture2D> ScreenShotTake(){
          var texture =  lineEditor.ScreenShotTake();
          return texture;
     }
     
     public void OnDrag(Vector2 position){
          if(_selectedTool == ToolType.Draw)
               lineEditor.Draw(position);
     }

     public void OnDrop(){
          if(_selectedTool == ToolType.Draw)
               lineEditor.StopDraw();
     }

     public void OnPointerDown(Vector2 position){
          if(_selectedTool == ToolType.Stickers)
               stickerEditor.CreateSticker(position);
     }

     public void OnPointerUp(){
     }

     public void SelectArrow(){
          _selectedTool = ToolType.Draw;
          lineEditor.SetArrow();
     }

     public void LineSelect(){
          _selectedTool = ToolType.Draw;
          lineEditor.SetLine();
     }

     public void StickerSelect(){
          _selectedTool = ToolType.Stickers;
     }
     
     public void SetColor(Color color){
          stickerEditor.SetColor(color);
          lineEditor.SetColor(color);
     }
     
     public void Undo()
     {
          lineEditor.Undo();
     }
     
     public void Clear(){
          
     }
}
