using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    private void Update()
    {
        
        HandleEscape();
    }
    public void ShowInGameMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        if (!menuPanel.activeSelf)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowInGameMenu();
        }
    }
}
