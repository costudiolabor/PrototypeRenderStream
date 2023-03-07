using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


namespace PaintEditor
{
    [Serializable]
    public class DetectorPlace
    {
        [SerializeField] private float offSetZ = 0.2f;
        [SerializeField] private float rayLength = 1000.0f;

        private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();
        private ARRaycastManager _rayCastManager;
        private Camera _cameraMain;
        private bool _isHitRayCast;

        public void Init(Camera cameraMain, ARRaycastManager rayCastManager)
        {
            _cameraMain = cameraMain;
            this._rayCastManager = rayCastManager;
        }
        
        public Vector3 GetPositionPlace(Vector2 touchPosition, out bool isHitRayCast)
        {
            //var pointPosition = RayFromCamera(touchPosition);
            var pointPosition = RayFromARCamera(touchPosition);
            isHitRayCast = _isHitRayCast;
           // Debug.Log("Pos = " + pointPosition);
            return new Vector3(pointPosition.x, pointPosition.y, pointPosition.z - offSetZ);
        }

        public Vector3 RayFromCamera(Vector3 touchPosition)
        {
            var ray = _cameraMain.ScreenPointToRay(touchPosition);
            _isHitRayCast = Physics.Raycast(ray, out var hit, rayLength);
           // Debug.Log("_isHit = " + _isHitRayCast);
            return hit.point;
        }
        
        public Vector3 RayFromARCamera(Vector3 touchPosition)
        {
            _isHitRayCast = _rayCastManager.Raycast(new Vector2(touchPosition.x, touchPosition.y), _raycastHits, TrackableType.Planes);
            //_isHitRayCast = _rayCastManager.Raycast(new Vector2(touchPosition.x, touchPosition.y), _raycastHits, TrackableType.FeaturePoint);
            return _isHitRayCast ?_raycastHits[0].pose.position : new Vector3(0,0,0);
        }
    }
}
