using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gachaButton;
    [SerializeField] private GameObject katowerButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject unoUnoButton;
    [SerializeField] private GameObject dosUnoButton;
    [SerializeField] private GameObject unoTresButton;
    [SerializeField] private GameObject dosCuatroButton;
    [SerializeField] private GameObject backButton;
    public void Play()
    {
        background.SetActive(!background.activeSelf);
        playButton.SetActive(!playButton.activeSelf);
        gachaButton.SetActive(!gachaButton.activeSelf);
        katowerButton.SetActive(!katowerButton.activeSelf);
        exitButton.SetActive(!exitButton.activeSelf);
        unoUnoButton.SetActive(!unoUnoButton.activeSelf);
        dosUnoButton.SetActive(!dosUnoButton.activeSelf);
        backButton.SetActive(!backButton.activeSelf);
        unoTresButton.SetActive(!unoTresButton.activeSelf);
        dosCuatroButton.SetActive(!dosCuatroButton.activeSelf);
    }
    public void NivelUnoUno()
    {
        SceneManager.LoadScene(1);
    }
    public void NivelUnoDos()
    {
        SceneManager.LoadScene(2);
    }
    public void NivelUnoTres()
    {
        SceneManager.LoadScene(3);
    }
    public void NivelUnoCuatro()
    {
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
