using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class UIManager : MonoBehaviour
{
    public bool IfAchievement;
    public Animator AchievementAnimator;

    public RingMenu RingMenuPrefab;
    protected RingMenu RingMenuInstance;

    public GameObject canvas;
    private GameObject scrollbar;
    public Text AchievementText;
    public Text DescriptionText;
    private int phase;

    private Animator Phase01;
    private Animator Phase02;
    private Animator Phase03;
    private Animator Phase04;

    private bool IfWin;
    private bool IfLose;

    private GameObject LosePanel;
    private GameObject WinPanel;

    public string[] Achievement = { "杀人如麻", "钓条大的", "钩无虚发", "初入职场", "牛刀小试", "如鱼得水", "大资本家" };
    public string[] Description = { "100名员工已经累死在了你的公司, 猝死的，被拍死的，被屎憋死的应有尽有", "成功钩住十个传奇品质打工人。WOW！金色打工人！", "连续命中20钩！/到爸爸这来！", "第一季度公司产值达标！", "第二季度公司产值达标！", "第三季度公司产值达标！", "恭喜！第四个季度的产值也成功达标！你俨然是一位合格的资本家了呢！" };
    public int length;

    void Start()
    {
        length = Achievement.Length;
        IfAchievement = false;
        phase = 0;
        IfWin = false;
        IfLose = false;
        scrollbar = GameObject.Find("Scrollbar");
        Phase01 = GameObject.Find("Phase01").GetComponent<Animator>();
        Phase02 = GameObject.Find("Phase02").GetComponent<Animator>();
        Phase03 = GameObject.Find("Phase03").GetComponent<Animator>();
        Phase04 = GameObject.Find("Phase04").GetComponent<Animator>();
        AchievementText = GameObject.Find("AchieveName").GetComponent<Text>();
        DescriptionText = GameObject.Find("Description").GetComponent<Text>();
        WinPanel = GameObject.Find("WinPanel");
        LosePanel = GameObject.Find("LosePanel");
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    void Update()
    {
        float money = GameInstance.Instance.Money;
        int[] levelMoney = { 6000, 15000, 36000, 72000 };
        float process;
        if (money <= levelMoney[0])
            process = 0.25f * money / levelMoney[0];
        else if (money <= levelMoney[1])
            process = 0.25f + 0.25f * (money - levelMoney[0]) / (levelMoney[1] - levelMoney[0]);
        else if (money <= levelMoney[2])
            process = 0.5f + 0.25f * (money - levelMoney[1]) / (levelMoney[2] - levelMoney[1]);
        else
            process = 0.75f + 0.25f * (money - levelMoney[2]) / (levelMoney[3] - levelMoney[2]);

        //Debug.Log(process);
        scrollbar.GetComponent<Scrollbar>().size = process;
        #region
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    phase = 1;
        //    //PlayAnim(Phase01);
        //    Phase01.SetBool("IfGlow", true);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    phase = 2;
        //    //PlayAnim(Phase02);
        //    Phase02.SetBool("IfGlow", true);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    phase = 3;
        //    //PlayAnim(Phase03);
        //    Phase03.SetBool("IfGlow", true);
        //}
        #endregion
        if (process >= 0.25f && phase == 0)
            if (process >= 0.25f && phase == 0)
            {
                phase = 1;
                Phase01.SetBool("IfGlow", true);
            }
            else if (process >= 0.5f && phase == 1)
            {
                phase = 2;
                Phase02.SetBool("IfGlow", true);
            }
            else if (process >= 0.75f && phase == 2)
            {
                phase = 3;
                Phase03.SetBool("IfGlow", true);
            }
            else if (process >= 1f && phase == 3)
            {
                phase = 4;
                Phase04.SetBool("IfGlow", true);
            }

        for (int i = 0; i < (AchievementList.Instance.Achievmenetlist.Length - 1); i++)
        {
            if (AchievementList.Instance.Achievmenetlist[i] == statues.Achieve)
            {
                AchievementText.text = Achievement[i];
                DescriptionText.text = Description[i];
                IfAchievement = true;
            }
            if (IfAchievement == true)
            {
                PlayAnim(AchievementAnimator);
                AchievementList.Instance.Achievmenetlist[i] = statues.Displayed;
                IfAchievement = false;
            }
        }
        // End Judgement !!!
        if (GameInstance.Instance.win == true)
        {
            //Debug.Log("W");
            WinPanel.gameObject.SetActive(true);
            IfWin = true;
            Time.timeScale = 0;
        }
        if (GameInstance.Instance.lose == true)
        {
            //Debug.Log("L");
            LosePanel.gameObject.SetActive(true);
            IfLose = true;
            Time.timeScale = 0;
        }
        if (IfLose || IfWin)
        {
            if(Input.GetKey(KeyCode.Q))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
    public void Ringclicked()
    {
        //RingMenuInstance = Instantiate(RingMenuPrefab, FindObjectOfType<Canvas>().transform);
        RingMenuInstance = Instantiate(RingMenuPrefab, GameObject.Find("WrenCanvas").transform);
        RingMenuInstance.callback = MenuClick;
    }
    private void MenuClick(string path)
    {
        Debug.Log(path);
    }

    public void PlayAnim(Animator animator)
    {
        StartCoroutine(PlayAnimation(animator));
    }
    //public void PlayAnim01(Animator animator)
    //{
    //    StartCoroutine(PlayAnimation01(animator));
    //}
    public IEnumerator PlayAnimation(Animator animator)
    {
        animator.SetBool("IfPlay", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("IfPlay", false);
    }
    //public IEnumerator PlayAnimation01(Animator animator)
    //{
    //    animator.SetBool("IfGlow", true);

    //    yield return new WaitForSeconds(0.1f);

    //    animator.SetBool("IfGlow", false);
    //}
}
