using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    public int level = 0;
    
    public int WorkSpace = 0;
    public bool isfull = false;

    public float Money = 0;
    public float LevelMoney = 0;

    public float StartTimer = 0.0f;
    public float LevelTime = 0.0f;

    public int DeadEmployee = 0;
    public int DeadEmployeebyPYP = 0;

    public int levelMoneyFactor = 2;
    //public 
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartTimer = Time.time;
        LevelTime = 180;
        checklevel();
    }

    void Update()
    {
        checklevel();
        if (Time.time - StartTimer > LevelTime) { }
            //bool lose = true;
    }

    void checklevel()
    {
        if (level == 4) { }
            //bool winning = true;
        else
        {
            if (Money > LevelMoney)
            {
                level++;
                StartTimer = Time.time;
                LevelMoney = (levelMoneyFactor) * 3000;
                WorkSpace = WorkSpace + 2 + level;
                levelMoneyFactor += WorkSpace;
            }
        }
    }
}
