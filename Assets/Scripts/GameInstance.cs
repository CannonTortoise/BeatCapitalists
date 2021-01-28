using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private get; }

    public int CurrentEmployee = 0;
    public int WorkSpace = 5;
    public int Money = 0;
    public int LevelMoney = 0;
    public float Timer = 0.0;
    public float LevelTime = 0.0;
    public bool Iffull = false;

    public int DeadEmployee = 0;
    //public 

}
