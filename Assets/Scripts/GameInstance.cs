﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    [Header("Class Settings")]
    public int level = 0;
    public int WorkSpace = 0;
    public bool isfull = false;
    public float Money = 0;
    public float LevelMoney = 0;
    public int levelMoneyFactor = 2;
    public bool lose = false;
    public bool win = false;

    [Header("Timer")]
    public float StartTimer = 0.0f;
    public float LevelTime = 0.0f;
    public float RemainTime = 0.0f;
    public string minutes;
    public string seconds;

    [Header("Achievements")]
    public int DeadEmployee = 0;
    public int DeadEmployeebyPYP = 0;
    //夺命连环20钩
    public int ContinueHookCount = 0;
    public bool ifachievedHookCount = false;
    //贪心不足
    public bool Sogreedy = false;
    //钓条大的
    public int LegendaryEmployee = 0;
    public bool LegendaryAchievement = false;

    [Header("GameObjects")]
    public GameObject[] CloseWorkSpaces;
    public GameObject PauseMenu;


    void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartTimer = Time.time;
        LevelTime = 0;
        RemainTime = 0.0f;
        checklevel();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Money += 1000;
        }
        checklevel();
        float t = Time.time - StartTimer;
        minutes = ((int)(LevelTime - t) / 60).ToString();
        seconds = ((int)(LevelTime - t) % 60).ToString();
        Money = Mathf.Round(Money * 100f) / 100f;                   //round up to 2 decimal, in case of n.9999

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
        }

        if (Time.time - StartTimer > LevelTime) { lose = true; }

    }

    void checklevel()
    {
        if (level == 5)
        {
            SoundEffectManager.playSound(2);
            win = true;
            level += 1;
        }
        else
        {
            if (Money >= LevelMoney)
            {
                level++;
                RemainTime = LevelTime - (Time.time - StartTimer);
                Debug.Log("" + RemainTime);
                StartTimer = Time.time;
                LevelTime = 180 + RemainTime;
                LevelMoney = (levelMoneyFactor) * 3000;
                WorkSpace = WorkSpace + 2 + level;
                levelMoneyFactor += WorkSpace;
                UpdateWorkSpace();
            }
        }
    }

    void UpdateWorkSpace()
    {
        if(GameInstance.Instance.WorkSpace > 18)
        {
            GameInstance.Instance.WorkSpace = 18;

        }
        int count = GameInstance.Instance.WorkSpace;
        for (int i = 0; i < count; i++)
        {
            CloseWorkSpaces[i].SetActive(false);
        }
    }

    public void checkhookcount()
    {
        if (ContinueHookCount >= 20)
        {
            ifachievedHookCount = true;
            SoundEffectManager.playSound(1);
        }
    }

    public void checkLegendary()
    {
        if (LegendaryEmployee >= 10)
        {
            LegendaryAchievement = true;
            SoundEffectManager.playSound(1);
        }
    }
}