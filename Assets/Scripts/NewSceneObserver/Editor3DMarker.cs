using System;
using UnityEngine;

[Serializable]
public class Editor3DMarker : ViewOperator<Editor3DMarkerView>
{
    [SerializeField] private HandlerMarker handlerMarker;
    [SerializeField] private float rayLength = 1000.0f;
    
    private Camera _cameraMain;
    private bool _isHitRayCast;

    public void Initialize()
    {
        _cameraMain = Camera.main;
        base.CreateView();
        view.Initialize();
        handlerMarker.Initialize();
        handlerMarker.SelectObjectEvent += view.OpenPanelEditMarker;
        view.PointerDownEvent += OnPointerDown;
        view.DeleteMarker3DEvent += DeleteMarker;
        view.ColorChangedEvent += SetColor;
    }
   
    public void ViewOpen() {
        view.Open();
    }
  
    public void ViewClose() {
        view.ForceClose();
    }
    
    public void OnPointerDown(Vector2 position) {
        RaycastHit raycastHit = RayFromCamera(position);
        if (_isHitRayCast) {
            IMarker3D marker3D = raycastHit.transform.GetComponent<IMarker3D>();
            marker3D.SelectObject();
        }
        else handlerMarker.CreateMarker(position);
    }
    
    public RaycastHit RayFromCamera(Vector3 touchPosition) {
        var ray = _cameraMain.ScreenPointToRay(touchPosition);
        _isHitRayCast = Physics.Raycast(ray, out var hit, rayLength);
        return hit;
    }

    public void SetColor(Color color) {
        handlerMarker.SetColor(color);
    }

    public void Clear() {
        handlerMarker.Clear();
    }

    private void DeleteMarker() {
        handlerMarker.DeleteMarker();
        view.Close();
    }
    
    public void CharInput(char charInput) {
        view.SetCharInputField(charInput);
    }

  
}
