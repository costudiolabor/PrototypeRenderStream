//-----------------------------------------------------------------------
// <copyright file="OrientedReticle.cs" company="Google LLC">
//
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

using System;
using UnityEngine;

[Serializable]
public class ObjectOrientation
{
    //private const float _outlierDepthRatio = 0.2f;
    //private const int _windowRadiusPixels = 2;
    // public Matrix4x4 RotationByPortriat
    // {
    //     get
    //     {
    //         switch (Screen.orientation)
    //         {
    //             case ScreenOrientation.Portrait:
    //                 return Matrix4x4.Rotate(Quaternion.identity);
    //             case ScreenOrientation.LandscapeLeft:
    //                 return Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90));
    //             case ScreenOrientation.PortraitUpsideDown:
    //                 return Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180));
    //             case ScreenOrientation.LandscapeRight:
    //                 return Matrix4x4.Rotate(Quaternion.Euler(0, 0, 270));
    //             default:
    //                 return Matrix4x4.Rotate(Quaternion.identity);
    //         }
    //     }
    // }

    [SerializeField] private float offSetDistance;

    public void Initialize()
    {
        DepthSource.SwitchToRawDepth(false);
    }

    public Vector3 GetPositionAR(Vector2 touchPosition)
    {
        Vector3 result = default;
        if (!DepthSource.Instance) return result;
        Vector2 screenPosition = CalculateTouchScreenToScreenPosition(touchPosition);
        result = CalculatePosition(screenPosition);
        return result;
    }

    // private void UpdateTransform(Transform transformObject, Vector2 screenPosition)
    // {
    //     try
    //     {
    //         float distance = ComputeCenterScreenDistance(screenPosition);
    //         if (distance > DepthSource.InvalidDepthValue)
    //         {
    //             Vector3 translation = DepthSource.ARCamera.ScreenToWorldPoint(new Vector3(
    //                 Screen.width * screenPosition.x,
    //                 Screen.height * screenPosition.y,
    //                 distance));
    //             _distance = distance;
    //             transformObject.position = translation;
    //         }
    //
    //         Quaternion? orientation = ComputeCenterScreenOrientation(screenPosition);
    //         if (orientation != null) {
    //             transformObject.rotation = Quaternion.Slerp(orientation.Value,
    //                         transform.rotation,
    //                         Interpolation * Time.deltaTime * Application.targetFrameRate);
    //         }
    //     }
    //     catch (InvalidOperationException)
    //     {
    //         // Intentional pitfall, depth values were invalid.
    //     }
    // }

    // private void UpdateTouchScreen()
    // {
    //      Debug.Log("UpdateTouchScreen");
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             Vector2 screenPosition = CalculateTouchScreenToScreenPosition(touch.position);
    //             SetPlane(screenPosition);
    //             Vector3 currentPosition = CalculatePosition(screenPosition);
    //              markers.CreateObject(currentPosition);
    //             Vector3 normal = RotationByPortriat *
    //             ComputeNormalMapFromDepthWeightedMeanGradient(screenPosition);
    //             normal = DepthSource.ARCamera.transform.TransformDirection(normal);
    //             _plane = new Plane(normal, currentPosition);
    //            lineCustom.CreateGameObject(currentPosition);
    //         }
    //
    //         if (touch.phase == TouchPhase.Moved)
    //         {
    //             Vector2 screenPosition = CalculateTouchScreenToScreenPosition(touch.position);
    //             Vector3 currentPosition = CalculatePosition(screenPosition);
    //            currentPosition = _plane.ClosestPointOnPlane(currentPosition);
    //             lineCustom.Draw(currentPosition);
    //         }
    //
    //         if (touch.phase == TouchPhase.Ended)
    //         {
    //           lineCustom.EndDraw();
    //         }
    //     }
    // }
    
    private Vector3 CalculatePosition(Vector2 screenPosition)
    {
        Vector3 position = default;
        try
        {
            float distance;
            distance = ComputeCenterScreenDistance(screenPosition) - offSetDistance;
            if (distance > DepthSource.InvalidDepthValue)
            {
                position = DepthSource.ARCamera.ScreenToWorldPoint(new Vector3(
                    Screen.width * screenPosition.x,
                    Screen.height * screenPosition.y,
                    distance));
            }
        }
        catch (InvalidOperationException)
        {
            // Intentional pitfall, depth values were invalid.
        }
        return position;
    }

    private Vector2 CalculateTouchScreenToScreenPosition(Vector2 touchPosition)
    {
        Vector2 result;
        result.x = touchPosition.x / Screen.width;
        result.y = touchPosition.y / Screen.height;
        return result;
    }

    private float ComputeCenterScreenDistance(Vector2 screenPosition)
    {
        Vector2 depthMapPoint = screenPosition;
        if (!DepthSource.Initialized)
        {
            Debug.LogError("Depth source is not initialized");
            throw new InvalidOperationException("Depth source is not initialized");
        }
        short[] depthMap = DepthSource.DepthArray;
        float depthM = DepthSource.GetDepthFromUV(depthMapPoint, depthMap);
        if (depthM <= DepthSource.InvalidDepthValue)
        {
            Debug.LogError("Invalid depth value");
            throw new InvalidOperationException("Invalid depth value");
        }
        Vector3 viewspacePoint = DepthSource.ComputeVertex(depthMapPoint, depthM);
        return viewspacePoint.magnitude;
    }

    // private Quaternion? ComputeCenterScreenOrientation(Vector2 screenPosition)
    // {
    //     Vector3 normal = RotationByPortriat *
    //         ComputeNormalMapFromDepthWeightedMeanGradient(screenPosition);
    //     normal = DepthSource.ARCamera.transform.TransformDirection(normal);
    //     Vector3 right = Vector3.right;
    //     if (normal != Vector3.up)
    //     {
    //         right = Vector3.Cross(normal, Vector3.up);
    //     }
    //     Vector3 forward = Vector3.Cross(normal, right);
    //     Quaternion orientation = Quaternion.identity;
    //     orientation.SetLookRotation(forward, normal);
    //     return orientation;
    // }

    // private Vector3 ComputeNormalMapFromDepthWeightedMeanGradient(Vector2 screenUV)
    // {
    //     short[] depthMap = DepthSource.DepthArray;
    //     Vector2 depthUV = screenUV;
    //     Vector2Int depthXY = DepthSource.DepthUVtoXY(depthUV);
    //     float depth_m = DepthSource.GetDepthFromUV(depthUV, depthMap);
    //     if (depth_m == DepthSource.InvalidDepthValue)
    //     {
    //         throw new InvalidOperationException("Invalid depth value");
    //     }
    //     float neighbor_corr_x = 0.0f;
    //     float neighbor_corr_y = 0.0f;
    //     float outlier_distance_m = _outlierDepthRatio * depth_m;
    //     int radius = _windowRadiusPixels;
    //     float neighbor_sum_confidences_x = 0.0f;
    //     float neighbor_sum_confidences_y = 0.0f;
    //     for (int dy = -radius; dy <= radius; ++dy)
    //     {
    //         for (int dx = -radius; dx <= radius; ++dx)
    //         {
    //             if (dx == 0 && dy == 0)
    //             {
    //                 continue;
    //             }
    //             Vector2Int offset = new Vector2Int(dx, dy);
    //             int currentX = depthXY.x + offset.x;
    //             int currentY = depthXY.y + offset.y;
    //             float neighbor_depth_m = DepthSource.GetDepthFromXY(currentX, currentY, depthMap);
    //             float neighbor_confidence = 1.0f;
    //             if (neighbor_depth_m == 0.0)
    //             {
    //                 continue;
    //             }
    //             float neighbor_distance_m = neighbor_depth_m - depth_m;
    //             if (neighbor_confidence == 0.0f ||
    //                 Mathf.Abs(neighbor_distance_m) > outlier_distance_m)
    //             {
    //                 continue;
    //             }
    //             if (dx != 0)
    //             {
    //                 neighbor_sum_confidences_x += neighbor_confidence;
    //                 neighbor_corr_x += neighbor_confidence * neighbor_distance_m / dx;
    //             }
    //             if (dy != 0)
    //             {
    //                 neighbor_sum_confidences_y += neighbor_confidence;
    //                 neighbor_corr_y += neighbor_confidence * neighbor_distance_m / dy;
    //             }
    //         }
    //     }
    //     if (neighbor_sum_confidences_x == 0 && neighbor_sum_confidences_y == 0)
    //     {
    //         throw new InvalidOperationException("Invalid confidence value.");
    //     }
    //     float pixel_width_m = depth_m / DepthSource.FocalLength.x;
    //     float slope_x = neighbor_corr_x / (pixel_width_m * neighbor_sum_confidences_x);
    //     float slope_y = neighbor_corr_y / (pixel_width_m * neighbor_sum_confidences_y);
    //     Vector3 normal = new Vector3(-slope_y, -slope_x, -1.0f);
    //     normal.Normalize();
    //     return normal;
    // }
}
