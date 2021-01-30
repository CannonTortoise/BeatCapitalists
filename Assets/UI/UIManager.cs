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
    private int phase;

    private Animator Phase01;
    private Animator Phase02;
    private Animator Phase03;

    public string[] Achievement = { "杀人如麻", "钓条大的", "钩无虚发", "初入职场", "牛刀小试", "如鱼得水", "大资本家"};
    public string[] Description = { "100名员工已经累死在了你的公司, 猝死的，被拍死的，被屎憋死的应有尽有", "成功钩住十个传奇品质打工人。WOW！金色打工人！", "连续命中20钩！/到爸爸这来！", "第一季度公司产值达标！", "第二季度公司产值达标！", "第三季度公司产值达标！", "恭喜！第四个季度的产值也成功达标！你俨然是一位合格的资本家了呢！" };
    public int length;

    void Start()
    {
        length = Achievement.Length;
        IfAchievement = false;
        phase = 0;
        scrollbar = GameObject.Find("Scrollbar");
        Phase01 = GameObject.Find("Phase01").GetComponent<Animator>();
        Phase02 = GameObject.Find("Phase02").GetComponent<Animator>();
        Phase03 = GameObject.Find("Phase03").GetComponent<Animator>();

    }
    public void Exit()
    {
        Application.Quit();
    }
    void Update()
    {
        var process = GameInstance.Instance.Money / GameInstance.Instance.LevelMoney;
        //Debug.Log(process);
        scrollbar.GetComponent<Scrollbar>().size = GameInstance.Instance.Money / GameInstance.Instance.LevelMoney;
        #region
        if (Input.GetKey(KeyCode.Q))
        {
            phase = 1;
            //PlayAnim(Phase01);
            Phase01.SetBool("IfGlow", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            phase = 2;
            //PlayAnim(Phase02);
            Phase02.SetBool("IfGlow", true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            phase = 3;
            //PlayAnim(Phase03);
            Phase03.SetBool("IfGlow", true);
        }
        #endregion
        if (process >= 0.25 && phase == 0)
        {
            phase = 1;
            Phase01.SetBool("IfGlow", true);
        }
        else if (process >= 0.5 && phase == 1)
        {
            phase = 2;
            Phase02.SetBool("IfGlow", true);
        }
        else if (process >= 0.75 && phase == 2)
        {
            phase = 3;
            Phase03.SetBool("IfGlow", true);
        }

        if (Input.GetKey(KeyCode.P))
        {
            IfAchievement = true;
        }
        if (IfAchievement == true)
        {
            PlayAnim(AchievementAnimator);
            IfAchievement = false;
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
