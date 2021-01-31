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
    public float DefaultDamage = 5;                                //伤害值
    public int WorkStrength = 0;                                    //0 = 955, 1 = 996, 2 = 007
    public float FinalDamage;
    //public int CurrentEmployee = 0;

    [Header("Class Settings")]
    public GameObject[] prefabsByClass;
    //public Worker WorkingWorker;
    public int[] probabilityByClass;

    [Header("Gizmos")]
    public bool isGizmosOn = false;
    public Vector4 feasibleRegion;

    [Header("Timer")]   
    public float WorkTimer;                                         //间隔timer
    public float Countdown = 1;                                     //间隔频率

    public List<Worker> FreeWorkers = new List<Worker>();           //漫步的打工人
    public List<Worker> WorkingWorkers = new List<Worker>();        //工位上的打工人

    [SerializeField]
    public bool[] seats = new bool[18];                             //座位bitmap

    private Transform workersParent;

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
        UpdateDamage();
    }

    void InitWorkers()
    {
        workersParent = GameObject.Find("Workers").transform;
        //for (int i = 0; i < initNum; i++)
            //GenerateWorker();
    }

    //随机产生一个新的worker
    public void GenerateWorker(Route route)
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

        //Vector3 workerPos = Vector3.zero;
        //while (Mathf.Abs(workerPos.x) < feasibleRegion.x && Mathf.Abs(workerPos.y) < feasibleRegion.y)
        //{
        //    workerPos.x = Random.Range(-feasibleRegion.z, feasibleRegion.z);
        //    workerPos.y = Random.Range(-feasibleRegion.w, feasibleRegion.w);
        //}
        Vector3 workerPos = route.points[0].position;
        GameObject workerInstance = Instantiate(prefabsByClass[rclass], workerPos, new Quaternion(), workersParent);
        workerInstance.GetComponent<Worker>().SetRoute(route);
        FreeWorkers.Add(workerInstance.GetComponent<Worker>());
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
                WorkingWorkers[i].Work(false, FinalDamage);
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

    public void UpdateDamage()
    {
        FinalDamage = 2.5f * WorkStrength + DefaultDamage;
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

    public void SetWorkStrength(int strength)
    {
        WorkStrength = strength;
    }
}
