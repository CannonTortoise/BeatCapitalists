using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public Text[] Textboxes = new Text[7];

    void Start()
    {
        for (int i = 0; i < Textboxes.Length; i++)
        {
            Textboxes[i] = GameObject.Find(("PauseText" + i)).GetComponent<Text>();
        }
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        Time.timeScale = 0;
        for (int i = 0; i < (AchievementList.Instance.Achievmenetlist.Length - 1); i++)
        {
            if (AchievementList.Instance.Achievmenetlist[i] == statues.Displayed)
            {
                Color z = Textboxes[i].color;
                z.a = 1.0f;
                Textboxes[i].color = z;
            }
        }
    }

    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Menu");
    }
}
