﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum WorkerClass { 
    Rare = 0,
    Epic = 1,
    Legendary = 2
};

public class Worker : MonoBehaviour
{
    public string       wname;                      // 名称
    public WorkerClass  wclass = WorkerClass.Rare;  // 级别
    public double       HP = 100;                   // 生命值
    public int          productivity = 1;           // 生产力
    public int          ability = 0;                // 特殊能力
    public float        moveSpeed = 1f;

    public bool         isWorking = false;          // 是否被雇佣工作
    private bool        isGrabbed = false;          // 是否被抓走
    private double      currentHP;                  // 当前HP
    private Vector3     moveDir = Vector3.zero;
    private float       workTimer;                  // 当前
    public int          seat;                       // 当前座位

    private Route       route;                      // 行走路线
    private int         routePoint;                 // 当前路线点
    private Vector3     nextPointPos;               // 下一个路线点坐标

    public Image FillHealthbar; 
    public Image BorderHealthbar;
    public Image HighlightHealthbar;

    private GameObject coinPrefab;

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

        currentHP = HP;

        FillHealthbar.fillAmount = 0.0f;
        BorderHealthbar.fillAmount = 0.0f;
        HighlightHealthbar.fillAmount = 0.0f;

        coinPrefab = Resources.Load("Prefabs/Coin") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this != null)
        {
            if (!isWorking && !isGrabbed)
                UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPointPos, Time.deltaTime * moveSpeed);
    }

    public void Move()
    {
        //transform.position += Time.deltaTime * moveDir * moveSpeed;

        routePoint++;

        if (routePoint < route.points.Length)
        {
            //transform.position = route.points[routePoint].position;
            nextPointPos = route.points[routePoint].position;
        }
        
    }

    public void CheckRemovement()
    {
        if (routePoint >= route.points.Length - 1)
        {
            WorkersController.Instance.FreeWorkers.Remove(this);
            Destroy(gameObject);
        }
    }

    void BeGrabbed()
    {
        isGrabbed = true;
    }

    public void StartWork()
    {
        isGrabbed = false;
        isWorking = true;

        seat = WorkersController.Instance.FindSeat();
        if (seat != -1)
        {
            int x = seat % 5, y = seat / 5;
            transform.position = new Vector3((-1.8f + x*0.9f), (0.9f-y*0.9f), -2f);
            WorkersController.Instance.WorkingWorkers.Add(this);
            WorkersController.Instance.FreeWorkers.Remove(this);
        }
        else
            Debug.Log("没工位了，你妈炸了");

        FillHealthbar.fillAmount = 1.0f;
        BorderHealthbar.fillAmount = 1.0f;
        HighlightHealthbar.fillAmount = 1.0f;
    }

    public void Work(bool click, double damage)
        //bool是判定是否是拍一拍，金钱和damage挂钩
    {
        float addingmoney = (float)
            ((productivity * (1 - (WorkersController.Instance.WorkStrength * 0.2))) * damage);
        if(currentHP < damage)
            addingmoney = (float)
                ((productivity * (1 - (WorkersController.Instance.WorkStrength * 0.2))) * currentHP);

        if (!click)
        {
            currentHP -= damage;
            checkclickdead(false);
            GameInstance.Instance.Money += addingmoney;
        }
        else
        {
            currentHP -= damage;
            checkclickdead(true);
            GameInstance.Instance.Money += 0.9f*addingmoney;
        }//拍一拍只有0.9的收入

        FillHealthbar.fillAmount = (float)(currentHP / HP); 
        HighlightHealthbar.fillAmount = (float)(currentHP / HP);

        GameObject coin = Instantiate(coinPrefab, transform.position + new Vector3(0.08f, 0.5f,0), new Quaternion(),transform);
        StartCoroutine(DestroyCoin(coin));

        Debug.Log(GameInstance.Instance.Money);
    }

    IEnumerator DestroyCoin(GameObject coin)
    {
        yield return new WaitForSeconds(1f);
        Destroy(coin);
    }

    void BeClicked()
    {
        Work(true, WorkersController.Instance.FinalDamage);
        //老板拍了拍你
    }

    void checkclickdead(bool click)
    {
        if (currentHP <= 0)
        {
            GameInstance.Instance.DeadEmployee++;
            if (click)
            {
                GameInstance.Instance.DeadEmployeebyPYP++;
                GameInstance.Instance.Money -= (5 * productivity);
            }
            WorkersController.Instance.WorkingWorkers.Remove(this);
            WorkersController.Instance.seats.SetValue(false, seat);
            Destroy(this.gameObject);

        }
    }

    void OnMouseDown()
    {
        if(isWorking)
            BeClicked();
    }

    public void SetRoute(Route i_route)
    {
        route = i_route;
        routePoint = 0;
        nextPointPos = i_route.points[0].position;
    }
}
