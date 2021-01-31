using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Transform[] points;
    public Color routeColor;
    public bool displayRoute = true;
    public int sortingOrder = 0;

    private LineRenderer lineRenderer;

    private void OnDrawGizmos()
    {
        Gizmos.color = routeColor;
        for (int i = 0; i < points.Length - 1; i++)
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
    }

    private void Start()
    {

        if (displayRoute)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startColor = routeColor;
            lineRenderer.endColor = routeColor;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;

            lineRenderer.positionCount = points.Length;
            for (int i = 0; i < points.Length; i++)
                lineRenderer.SetPosition(i, points[i].position);
            
        }
    }

}
