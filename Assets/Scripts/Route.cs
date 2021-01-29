using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Transform[] points;
    public Color routeColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = routeColor;
        for (int i = 0; i < points.Length - 1; i++)
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
    }

}
