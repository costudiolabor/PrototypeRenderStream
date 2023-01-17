using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
        public GameObject placedObject;
    
        private GameObject _spawnedPlacedObject;
        private ARRaycastManager _raycastManager;

        private readonly List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();
    
        private void Awake()
        {
            _raycastManager = GetComponent<ARRaycastManager>();
        }

        private static bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount == 0)
            {
                touchPosition = default;
                return false;
            }

            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        private void Update()
        {
            if (!TryGetTouchPosition(out var touchPosition))
            {
                return;
            }

            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            if (!_raycastManager.Raycast(touchPosition, _raycastHits, TrackableType.PlaneWithinPolygon))
            {
                return;
            }

            var hitPose = _raycastHits[0].pose;
            if (!_spawnedPlacedObject)
            {
                _spawnedPlacedObject = Instantiate(placedObject, hitPose.position, hitPose.rotation);
            }
            _spawnedPlacedObject.transform.position = hitPose.position;
        }
    }
}