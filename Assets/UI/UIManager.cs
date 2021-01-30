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
    private float process; // 0-1
    private int phase;

    void Start()
    {
        IfAchievement = false;
        process = GameInstance.Instance.Money / GameInstance.Instance.LevelMoney;
        phase = 0;
        scrollbar = GameObject.Find("Scrollbar");
    }
    public void Exit()
    {
        Application.Quit();
    }
    void Update()
    {
        Debug.Log(process);
        scrollbar.GetComponent<Scrollbar>().size = process;
        if(process >= 0.25 && phase == 0)
        {
            phase = 1;
        }
        else if(process >= 0.5 && phase == 1)
        {
            phase = 2;
        }
        else if (process >= 0.5 && phase == 2)
        {
            phase = 3;
        }

        if (Input.GetKey(KeyCode.P))
        {
            //Debug.Log("P");
            IfAchievement = true;
        }
        if (IfAchievement == true)
        {
            //Debug.Log("Do P");
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
    public void MoneyUp()
    {
        Debug.Log("+");
    }
    private void MenuClick(string path)
    {
        Debug.Log(path);
    }

    public void PlayAnim(Animator animator)
    {
        StartCoroutine(PlayAnimation(animator));
    }
    public IEnumerator PlayAnimation(Animator animator)
    {
        animator.SetBool("IfPlay", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("IfPlay", false);
    }

}
