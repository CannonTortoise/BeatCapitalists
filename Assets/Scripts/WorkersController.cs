using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Worker;

public class WorkersController : MonoBehaviour
{
    public static WorkersController Instance { get; private set; }

    [Header("Basic Settings")]
    public int initNum = 10;
    public float moveSpeed = 1f;

    public int CurrentEmployee = 0;
    public int WorkStrength = 0;

    [Header("Class Settings")]
    public GameObject[] prefabsByClass;
    //public Worker WorkingWorker;
    public int[] probabilityByClass;

    [Header("Gizmos")]
    public bool isGizmosOn = false;
    public Vector4 feasibleRegion;

    [Header("Timer")]
    public float WorkTimer;
    public float Countdown = 1;

    private List<GameObject> workers = new List<GameObject>();
    public List<Worker> WorkingWorkers = new List<Worker>();

    [SerializeField]
    public bool[] seats = new bool[20];

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
        UpdateTimer();
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

    void UpdateTimer()
    {
        float t = Time.time - WorkTimer;
        if (t >= Countdown)
        {
            WorkTimer = Time.time;
            AssignWork();
        }
    }

    void AssignWork()
    {
        for (int i = 0; i < WorkingWorkers.Count; i++)
        {
            if (WorkingWorkers[i].isWorking)
            {
                WorkingWorkers[i].Work(false, 5);
            }
        }
    }

    public int FindSeat()
    {
        int returns = 0;
        while (returns < GameInstance.Instance.WorkSpace)
        {
            Debug.Log(returns);
            if (!seats[returns])
            {
                seats[returns]=true;
                return returns;
            }
            returns++;
        }
        return -1;
    }

    public void CheckSeats()
    {
        bool check = true;
        for (int i = 0; i < GameInstance.Instance.WorkSpace; i++)
        {
            check &= seats[i];
        }
        GameInstance.Instance.isfull = check;
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
