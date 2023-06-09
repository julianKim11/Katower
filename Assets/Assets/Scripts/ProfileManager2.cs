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
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("ProfileName", nameInput.text);
        currentProfileText.text = PlayerPrefs.GetString("ProfileName");
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
