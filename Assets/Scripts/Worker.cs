using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkerClass { 
    Rare = 0,
    Epic = 1,
    Legendary = 2
};

public class Worker : MonoBehaviour
{
    public string       wname;                      // 名称
    public WorkerClass  wclass = WorkerClass.Rare;  // 级别
    public int          HP = 100;                   // 生命值
    public int          productivity = 1;           // 生产力
    public int          ability = 0;                // 特殊能力
    public float        moveSpeed = 1f;

    private bool        isWorking = false;          // 是否被雇佣工作
    private bool        isGrabbed = false;          // 是否被抓走
    private int         currentHP;                  // 当前HP
    private Vector3     moveDir = Vector3.zero;
    private float       workTimer;                  // 当前

    // Start is called before the first frame update
    void Start()
    {
        int rnum = Random.Range(0, 2);
        if (Mathf.Abs(transform.position.x) < WorkersController.Instance.feasibleRegion.x &&
            Mathf.Abs(transform.position.y) > WorkersController.Instance.feasibleRegion.y)
        {
            if (rnum == 0)
                moveDir = Vector3.left;
            else
                moveDir = Vector3.right;
        }
        else if (Mathf.Abs(transform.position.x) > WorkersController.Instance.feasibleRegion.x &&
            Mathf.Abs(transform.position.y) < WorkersController.Instance.feasibleRegion.y)
        {
            if (rnum == 0)
                moveDir = Vector3.up;
            else
                moveDir = Vector3.down;
        }
        
        else if (transform.position.x > 0 && transform.position.y > 0)
        {
            if (rnum == 0)
                moveDir = Vector3.down;
            else
                moveDir = Vector3.left;
        }
        else if (transform.position.x > 0 && transform.position.y < 0)
        {
            if (rnum == 0)
                moveDir = Vector3.up;
            else
                moveDir = Vector3.left;
        }
        else if (transform.position.x < 0 && transform.position.y > 0)
        {
            if (rnum == 0)
                moveDir = Vector3.down;
            else
                moveDir = Vector3.right;
        }
        else
        {
            if (rnum == 0)
                moveDir = Vector3.up;
            else
                moveDir = Vector3.right;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWorking && !isGrabbed) 
            Move();
    }

    void Move()
    {
        transform.position += Time.deltaTime * moveDir * moveSpeed;
    }

    void BeGrabbed()
    {
        isGrabbed = true;
    }

    public void StartWork()
    {
        isGrabbed = false;
        isWorking = true;
        transform.position = new Vector3(Random.Range(0f, 3f), Random.Range(0f, 3f), -2f);
    }

    void Work()
    {
    }

    void BeClicked()
    {
    
    }

}
