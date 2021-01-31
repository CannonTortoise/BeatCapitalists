using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject DevelopersPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("TestWorkers");
    }

    public void OpenDevelopersPanel()
    {
        DevelopersPanel.SetActive(true);
    }

    public void CloseDevelopersPanel()
    {
        DevelopersPanel.SetActive(false);
    }
}
