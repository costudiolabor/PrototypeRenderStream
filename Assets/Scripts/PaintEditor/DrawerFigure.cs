using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


namespace PaintEditor
{
    [System.Serializable]
    public class DrawerFigure
    {
        private bool _isHitRayCast;
        private Figure _currentFigure;
        private Stack<Figure> _figures = new Stack<Figure>();
        [SerializeField] private DetectorPlace detectorPlace;
        
        public void Init(Camera cameraMain, ARRaycastManager rayCastManager)
        {
            detectorPlace = new DetectorPlace();
            detectorPlace.Init(cameraMain, rayCastManager);
        }

        public void StartTouch(Vector2 touchPosition)
        {
            var position = detectorPlace.GetPositionPlace(touchPosition,out _isHitRayCast);
            if (!_isHitRayCast) return;
            _currentFigure = new LineCustom();
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
            var position = detectorPlace.GetPositionPlace(touchPosition, out _isHitRayCast);
            if (!_isHitRayCast)
            {
                CancelTouch();
                return;
            }
            _currentFigure.Draw(position);
        }

        public void DeleteFigure()
        {
            Debug.Log("del");
            if (_figures.Count <= 0) return;
            var figure = _figures.Pop();
            figure.DestroyObject();
        }

        // public Vector3 GetPositionPlace(Vector2 touchPosition)
        // {
        //     var hit = RayFromCamera(touchPosition, 1000.0f);
        //     return new Vector3(hit.point.x, hit.point.y, hit.point.z - offSetZ);
        // }
        //
        // public RaycastHit RayFromCamera(Vector3 touchPosition, float rayLength)
        // {
        //     var ray = _cameraMain.ScreenPointToRay(touchPosition);
        //     _isHitRayCast = Physics.Raycast(ray, out var hit, rayLength);
        //     return hit;
        // }

    }
}
