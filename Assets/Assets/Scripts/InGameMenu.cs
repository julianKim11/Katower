using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private LevelManager levelManager;
    private int sceneNumber;
    private void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }
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

        SceneManager.LoadScene(sceneNumber);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        levelManager.ResetLevelManager();
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
