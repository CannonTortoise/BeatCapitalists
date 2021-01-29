﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersController : MonoBehaviour
{
    public static WorkersController Instance { get; private set; }

    [Header("Basic Settings")]
    public int initNum = 10;
    public float moveSpeed = 1f;

    [Header("Class Settings")]
    public GameObject[] prefabsByClass;
    public int[] probabilityByClass;

    [Header("Gizmos")]
    public bool isGizmosOn = false;
    public Vector4 feasibleRegion;

    private List<GameObject> workers = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitWorkers();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWorkersPosition();
    }

    void InitWorkers()
    {
        Transform parent = GameObject.Find("Workers").transform;
        for (int i = 0; i < initNum; i++)
        {
            int rnum = Random.Range(0, 100);
            int rclass = 0;
            int count = 0;

            for (; rclass < probabilityByClass.Length; rclass++)
            {
                count += probabilityByClass[rclass];
                if (rnum < count)
                    break;
            }

            Vector3 workerPos = Vector3.zero;
            while (Mathf.Abs(workerPos.x) < feasibleRegion.x && Mathf.Abs(workerPos.y) < feasibleRegion.y) 
            {
                workerPos.x = Random.Range(-feasibleRegion.z, feasibleRegion.z);
                workerPos.y = Random.Range(-feasibleRegion.w, feasibleRegion.w);
            }
            GameObject workerInstance = Instantiate(prefabsByClass[rclass], workerPos, new Quaternion(), parent);
            workerInstance.transform.position = workerPos;
            workers.Add(workerInstance);
        }
    }

    void UpdateWorkersPosition()
    { 
    
    }

    private void OnDrawGizmos()
    {
        if (isGizmosOn)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(feasibleRegion.x, feasibleRegion.y), new Vector3(-feasibleRegion.x, feasibleRegion.y));
            Gizmos.DrawLine(new Vector3(-feasibleRegion.x, feasibleRegion.y), new Vector3(-feasibleRegion.x, -feasibleRegion.y));
            Gizmos.DrawLine(new Vector3(-feasibleRegion.x, -feasibleRegion.y), new Vector3(feasibleRegion.x, -feasibleRegion.y));
            Gizmos.DrawLine(new Vector3(feasibleRegion.x, -feasibleRegion.y), new Vector3(feasibleRegion.x, feasibleRegion.y));
            Gizmos.DrawLine(new Vector3(feasibleRegion.z, feasibleRegion.w), new Vector3(-feasibleRegion.z, feasibleRegion.w));
            Gizmos.DrawLine(new Vector3(-feasibleRegion.z, feasibleRegion.w), new Vector3(-feasibleRegion.z, -feasibleRegion.w));
            Gizmos.DrawLine(new Vector3(-feasibleRegion.z, -feasibleRegion.w), new Vector3(feasibleRegion.z, -feasibleRegion.w));
            Gizmos.DrawLine(new Vector3(feasibleRegion.z, -feasibleRegion.w), new Vector3(feasibleRegion.z, feasibleRegion.w));

        }
    }
}