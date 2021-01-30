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

    void Start()
    {
        IfAchievement = false;
    }
    public void Exit()
    {
        Application.Quit();
    }
    void Update()
    {
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
        //Debug.Log("111");
        RingMenuInstance = Instantiate(RingMenuPrefab, FindObjectOfType<Canvas>().transform);
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
