using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    public Route[] routes;
    public float routeUpdateTime = 0.5f;
    public int probabilityGenerateNewWorker = 20;
    public bool displayRoute = true;

    private float routeTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (displayRoute)
        {
            for (int i = 0; i < routes.Length; i++)
            {
                for(int j = 0; j < routes[i].points.Length - 1; j++) 
                {
                    Vector3 startPos = routes[i].points[j].position;
                    Vector3 endPos = routes[i].points[j + 1].position;
                    Debug.DrawLine(startPos, endPos, routes[i].routeColor,100000f);
                }
            }
        }

        routeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        routeTimer -= Time.deltaTime;
        if (routeTimer <= 0)
        {
            routeTimer = routeUpdateTime;
            MoveAllFreeWorkers();
            GenerateNewWorkers();
        }
    }

    void MoveAllFreeWorkers()
    {
        List<Worker> freeWorkers = WorkersController.Instance.FreeWorkers;
        for (int i = 0; i < freeWorkers.Count; i++)
            freeWorkers[i].CheckRemovement();
        for (int i = 0; i < freeWorkers.Count; i++) 
            freeWorkers[i].Move();
    }

    void GenerateNewWorkers()
    {
        for(int i = 0; i < routes.Length; i++)
        {
            int rnum = Random.Range(0, 100);
            if (rnum < probabilityGenerateNewWorker)
                WorkersController.Instance.GenerateWorker(routes[i]);
        }
    }
}
