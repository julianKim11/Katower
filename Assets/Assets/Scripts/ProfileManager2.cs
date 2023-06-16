using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager2 : MonoBehaviour
{
    public InputField nameInput;
    private string defaultName = "Invitado";

    public Text currentProfileText;
    public GameObject profilePanel;
    public GameObject yourTowersPanel;
    public Button closeYourTowersPanel;
    public Button yourTowers;
    public GachaSystem gachaSystem;
    void Start()
    {
        LoadSettings();
    }
    public void ToggleYourTowerPanel()
    {
        profilePanel.SetActive(!profilePanel.activeSelf);
    }
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ProfileName"))
        {
            currentProfileText.text = PlayerPrefs.GetString("ProfileName");
        } 
        else
        {
            nameInput.text = defaultName;
            PlayerPrefs.SetString("ProfileName", defaultName);
        }
    }
    public void SetNamePref()
    {
        Debug.Log("DELETEALL");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("ProfileName", nameInput.text);
        currentProfileText.text = PlayerPrefs.GetString("ProfileName");
        gachaSystem.ResetTowerCount();
        gachaSystem.UpdateTowerUI();
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    public void CloseYourTowersPanel()
    {
        yourTowersPanel.SetActive(false);
    }
    public void OpenYourTowers()
    {
        yourTowersPanel.SetActive(true);
    }
}