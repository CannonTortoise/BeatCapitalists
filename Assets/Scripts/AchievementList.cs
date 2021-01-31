using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statues
{
    NotAchieve = 0,
    Achieve = 1,
    Displayed = 2,
}

public class AchievementList : MonoBehaviour
{
    public static AchievementList Instance { get; private set; }

    public statues[] Achievmenetlist = new statues[8];
    
    void Start()
    {
        for (int i = 0; i < Achievmenetlist.Length; i++)
            Achievmenetlist[i] = statues.NotAchieve;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            Achievmenetlist[0] = statues.Achieve;
        if (GameInstance.Instance.DeadEmployee == 100 && 
            Achievmenetlist[0] == statues.NotAchieve)
            Achievmenetlist[0] = statues.Achieve;           //杀人如麻
        if (GameInstance.Instance.LegendaryAchievement &&
            Achievmenetlist[1] == statues.NotAchieve)
            Achievmenetlist[1] = statues.Achieve;           //钓条大鱼
        if (GameInstance.Instance.ifachievedHookCount &&
            Achievmenetlist[2] == statues.NotAchieve)
            Achievmenetlist[2] = statues.Achieve;           //钩无虚发
        if (GameInstance.Instance.level == 2 &&
            Achievmenetlist[3] == statues.NotAchieve)
            Achievmenetlist[3] = statues.Achieve;           //初入职场
        if (GameInstance.Instance.level == 3 && 
            Achievmenetlist[4] == statues.NotAchieve)
            Achievmenetlist[4] = statues.Achieve;           //牛刀小试
        if (GameInstance.Instance.level == 4 &&
            Achievmenetlist[5] == statues.NotAchieve)
            Achievmenetlist[5] = statues.Achieve;           //如鱼得水
        if (GameInstance.Instance.level == 5 &&
            Achievmenetlist[6] == statues.NotAchieve)
            Achievmenetlist[6] = statues.Achieve;           //大资本家
        if (GameInstance.Instance.Sogreedy && 
            Achievmenetlist[7] == statues.NotAchieve)
            Achievmenetlist[7] = statues.Achieve;           //贪心不足

    }

    void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
}
