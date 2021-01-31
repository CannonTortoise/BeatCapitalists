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
        goalText.text = "" + GameInstance.Instance.LevelMoney;
        moneyText.text = "" + GameInstance.Instance.Money;
        timeText.text = "" + GameInstance.Instance.minutes + ":" + GameInstance.Instance.seconds;
        strengthText.text = "" + strengthButtonText[WorkersController.Instance.WorkStrength].text;
    }
}
