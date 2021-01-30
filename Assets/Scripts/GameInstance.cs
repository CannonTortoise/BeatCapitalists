using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    public int level = 0;
    
    public int WorkSpace = 5;
    public bool isfull = false;

    public float Money = 0;
    public float LevelMoney = 0;

    public float StartTimer = 0.0f;
    public float LevelTime = 0.0f;

    public int DeadEmployee = 0;
    public int DeadEmployeebyPYP = 0;
    //public 
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LevelMoney = 20000;
        StartTimer = Time.time;
        LevelTime = 180;
    }

    void Update()
    {
        checklevel();
        if (Time.time - StartTimer > LevelTime) { }
            //bool lose = true;
    }

    void checklevel()
    {
        if (level == 3) { }
            //bool winning = true;
        else
        {
            if (Money > LevelMoney)
            {
                level++;
                WorkSpace += 5;
                StartTimer = Time.time;
                LevelMoney = 20000 * Mathf.Pow((level + 1), 1.2f);
            }
        }
    }
}
