using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager instance;
    public static Profile currentProfile;
    private string currentProfileKey = "CurrentProfile";
    private string lastProfileKey = "LastProfile";
    private Dictionary<string, Profile> profiles = new Dictionary<string, Profile>();

    public InputField profileNameInput;
    public Text currentProfileText;
    public GameObject ProfilePanel;
    public GameObject yourTowersPanel;
    public Button yourTowers;
    public Button closeYourTowersPanel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProfiles();

            if (!PlayerPrefs.HasKey(currentProfileKey))
            {
                currentProfile = null;
            }
            else
            {
                if (PlayerPrefs.HasKey(lastProfileKey))
                {
                    string lastProfileName = PlayerPrefs.GetString(lastProfileKey);
                    if (profiles.ContainsKey(lastProfileName))
                    {
                        currentProfile = profiles[lastProfileName];
                        currentProfileText.text = lastProfileName;
                        Debug.Log("Perfil actual cargado: " + lastProfileName);
                    }
                }
            }

            LoadCurrentProfile();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        closeYourTowersPanel.onClick.AddListener(CloseYourTowersPanel);
        yourTowers.onClick.AddListener(OpenYourTowers);
    }
    public void CloseYourTowersPanel()
    {
        yourTowersPanel.SetActive(false);
    }
    public void OpenYourTowers()
    {
        yourTowersPanel.SetActive(true);
    }
    public static Profile GetCurrentProfile()
    {
        if(instance == null)
        {
            Debug.LogWarning("ProfileManager instance is null.");
            return null;
        }

        if (currentProfile == null)
        {
            Debug.LogWarning("No hay un perfil actual.");
            return null;
        }

        return currentProfile;
    }
    private void LoadProfiles()
    {
        // Cargar perfiles guardados de PlayerPrefs
        if (PlayerPrefs.HasKey("ProfileCount"))
        {
            int profileCount = PlayerPrefs.GetInt("ProfileCount");
            for (int i = 0; i < profileCount; i++)
            {
                string profileKey = "Profile_" + i.ToString();
                string profileData = PlayerPrefs.GetString(profileKey);
                Profile profile = JsonUtility.FromJson<Profile>(profileData);

                string estambreKey = "ProfileEstambre_" + profile.profileName;
                if (PlayerPrefs.HasKey(estambreKey))
                {
                    profile.estambre = PlayerPrefs.GetInt(estambreKey);
                }

                string acquiredTowersKey = "ProfileAcquiredTowers_" + profile.profileName;
                string acquiredTowersData = PlayerPrefs.GetString(acquiredTowersKey);
                List<string> acquiredTowers = JsonUtility.FromJson<List<string>>(acquiredTowersData);

                profile.acquiredTowers = acquiredTowers;
                profiles.Add(profile.profileName, profile);
            }
            if (profiles.Count == 0)
            {
                GachaSystem gachaSystem = FindObjectOfType<GachaSystem>();
                if (gachaSystem != null)
                {
                    gachaSystem.DisableRollButton();
                }
            }
        }
    }

    private void LoadCurrentProfile()
    {
        // Cargar el perfil actual guardado en PlayerPrefs
        if (PlayerPrefs.HasKey(currentProfileKey))
        {
            string currentProfileName = PlayerPrefs.GetString(currentProfileKey);
            if (profiles.ContainsKey(currentProfileName))
            {
                currentProfile = profiles[currentProfileName];
                currentProfileText.text = currentProfileName;
                Debug.Log("Perfil actual cargado: " + currentProfileName);

                GachaSystem gachaSystem = FindObjectOfType<GachaSystem>();
                if (gachaSystem != null)
                {
                    gachaSystem.ActivateProfile();
                    gachaSystem.UpdateUI();
                    gachaSystem.UpdateTowerUI();
                }
            }
        }
        else
        {
            currentProfileText.text = "Perfil actual: Ninguno";

            // Deshabilitar el botón "Roll" y restablecer las torres a 0
            GachaSystem gachaSystem = FindObjectOfType<GachaSystem>();
            if (gachaSystem != null)
            {
                gachaSystem.DisableRollButton();
                gachaSystem.ResetTowerCounts();
            }
        }
    }

    public void CreateProfile(string profileName)
    {
        string newProfileName = profileNameInput.text.Trim();
        if (!string.IsNullOrEmpty(newProfileName))
        {
            // Crear un nuevo perfil
            if (!profiles.ContainsKey(newProfileName))
            {
                Profile newProfile = new Profile(newProfileName);
                profiles.Add(newProfileName, newProfile);
                SaveProfiles();
                //SwitchProfile(newProfileName);
                currentProfile = newProfile;
                currentProfileText.text = newProfile.profileName;
                FindObjectOfType<GachaSystem>().ActivateProfile();

                ProfilePanel.SetActive(false);
                Debug.Log("Perfil creado: " + newProfileName);

                PlayerPrefs.SetString(lastProfileKey, newProfileName);
            }
            else
            {
                Debug.LogWarning("Ya existe un perfil con ese nombre: " + newProfileName);
            }
        }
        else
        {
            Debug.LogWarning("El nombre del perfil no puede estar vacío.");
        }
    }


    //public void SwitchProfile(string profileName)
    //{
    //    // Cambiar al perfil seleccionado
    //    if (profiles.ContainsKey(profileName))
    //    {
    //        currentProfile = profiles[profileName];
    //        PlayerPrefs.SetString(currentProfileKey, currentProfile.profileName);
    //        currentProfileText.text = "Perfil actual: " + currentProfile.profileName;
    //        // Actualizar el cuadro de texto con el perfil actual
    //        Debug.Log("Perfil cambiado a: " + currentProfile.profileName);
    //        GachaSystem gachaSystem = FindObjectOfType<GachaSystem>();
    //        gachaSystem.ActivateProfile();
    //        if (gachaSystem != null)
    //        {
    //            gachaSystem.towerCount.Clear();
    //            gachaSystem.towerCount["Luchador"] = currentProfile.luchadorCount;
    //            gachaSystem.towerCount["Enamorado"] = currentProfile.enamoradoCount;
    //            gachaSystem.towerCount["Trampero"] = currentProfile.tramperoCount;
    //            gachaSystem.UpdateTowerUI();
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No se encontró el perfil: " + profileName);
    //    }
    //}


    private void SaveProfiles()
    {
        //for (int i = 0; i < profiles.Count; i++)
        //{
        //    string profileKey = "Profile_" + i.ToString();
        //    string profileData = PlayerPrefs.GetString(profileKey);
        //    Debug.Log("PlayerPrefs Key: " + profileKey + ", Value: " + profileData);
        //}
        // Guardar perfiles en PlayerPrefs
        PlayerPrefs.SetInt("ProfileCount", profiles.Count);

        int index = 0;

        foreach (KeyValuePair<string, Profile> kvp in profiles)
        {
            string profileKey = "Profile_" + index.ToString();
            string profileData = JsonUtility.ToJson(kvp.Value);
            PlayerPrefs.SetString(profileKey, profileData);

            string estambreKey = "ProfileEstambre_" + kvp.Value.profileName;
            PlayerPrefs.SetInt(estambreKey, kvp.Value.estambre);

            string acquiredTowersKey = "ProfileAcquiredTowers_" + kvp.Value.profileName;
            string acquiredTowersData = JsonUtility.ToJson(kvp.Value.acquiredTowers);
            PlayerPrefs.SetString(acquiredTowersKey, acquiredTowersData);

            index++;
        }
        PlayerPrefs.Save();
        
    }


    public void DeleteProfile()
    {
        if (currentProfile != null)
        {
            GachaSystem gachaSystem = FindObjectOfType<GachaSystem>();
            string profileToDelete = currentProfile.profileName;
            profiles.Remove(profileToDelete);
            currentProfile = null;
            PlayerPrefs.DeleteKey(currentProfileKey);
            SaveProfiles();
            gachaSystem.DisableRollButton();

            if (gachaSystem != null)
            {
                gachaSystem.ResetTowerCounts();
            }

            if(MainManager.instance.estambre != 0)
            {
                MainManager.instance.estambre = 0;
            }

            // Actualizar el cuadro de texto con un mensaje adecuado
            currentProfileText.text = "Perfil actual: Ninguno";
            Debug.Log("Perfil borrado.");
           
            if (profiles.Count == 0)
            {
                if(gachaSystem != null)
                {
                    gachaSystem.DisableRollButton();
                }
            }
        }
        else
        {
            Debug.LogWarning("No hay un perfil actual para borrar.");
        }
    }
}

[System.Serializable]
public class Profile
{

    [JsonProperty("profileName")]public string profileName;
    public int luchadorCount;
    public int tramperoCount;
    public int enamoradoCount;
    public List<string> acquiredTowers;
    public int estambre;
    // Agrega los datos que deseas guardar para cada perfil

    public Profile(string name)
    {
        profileName = name;
        luchadorCount = 0;
        enamoradoCount = 0;
        tramperoCount = 0;
        // Inicializa los datos de perfil según tus necesidades
    }
}
