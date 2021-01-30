using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text goalText;
    public Text moneyText;
    public Text timeText;
    public Text strengthText;
    public Text[] strengthButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goalText.text = "当前目标：" + GameInstance.Instance.LevelMoney;
        moneyText.text = "当前金币：" + GameInstance.Instance.Money;
        timeText.text = "剩余时间：" + Mathf.Ceil(GameInstance.Instance.LevelTime - GameInstance.Instance.StartTimer);
        strengthText.text = "当前强度：" + strengthButtonText[WorkersController.Instance.WorkStrength].text;
    }
}
