using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using  UnityEngine.UI;

public class ScreenShoter : View , IPointerDownHandler
{
   [SerializeField] private ImageEditor imageEditor;

   
   public event Action PointerDownEvent;
   
   private UniTaskCompletionSource<Texture2D> _taskCompletionSource;
   
   public void InitializeView()
   {
      imageEditor.InitializeView();
      Debug.Log("Init");

       PointerDownEvent += OnClick;
            
     
   }
   
   public void OnPointerDown(PointerEventData eventData)
   {
      PointerDownEvent?.Invoke();
   }
   
   
   private  void OnClick()
   {
      Debug.Log("Click");
      // view.tools.Close();
      // _taskCompletionSource = new UniTaskCompletionSource<Texture2D>();
      // var texture = await imageEditor._screenshot.Take();
      // view.tools.Open();
      // _taskCompletionSource.TrySetResult(texture);
      // await imageEditor.EditProcess(texture);
   }
   
   
}
