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
}
