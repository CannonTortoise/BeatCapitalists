using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    public int Money = 0;
    public int LevelMoney = 0;

    public float LevelTime = 0;

    public int DeadEmployee = 0;
    public int DeadEmployeebyPYP = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
