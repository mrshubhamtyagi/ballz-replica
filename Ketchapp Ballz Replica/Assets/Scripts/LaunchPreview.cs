using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchPreview : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 dragStartPoint;
    private Vector3 pointOffset;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetStartPoint(Vector3 _worldPoint)
    {
        dragStartPoint = _worldPoint;
        lineRenderer.SetPosition(0, dragStartPoint);
    }

    public void SetEndPoint(Vector3 _worldPoint)
    {
        pointOffset = _worldPoint - dragStartPoint;
        Vector3 endPoint = transform.position + pointOffset;
        lineRenderer.SetPosition(1, endPoint);
    }

}
