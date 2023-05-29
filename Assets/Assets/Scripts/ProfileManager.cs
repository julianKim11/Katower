using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager instance;
    public static Profile currentProfile;
    private string currentProfileKey = "CurrentProfile";
    private Dictionary<string, Profile> profiles = new Dictionary<string, Profile>();

    public InputField profileNameInput;
    public Text currentProfileText;
    public Dropdown profileDropdown;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProfiles();
            LoadCurrentProfile();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        UpdateProfileDropdownOptions();
        profileDropdown.onValueChanged.AddListener(OnProfileDropdownValueChanged);
    }
    public static Profile GetCurrentProfile()
    {
        Debug.Log("asd");
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
                profiles.Add(profile.profileName, profile);
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
                currentProfileText.text = "Perfil actual: " + currentProfileName;
                Debug.Log("Perfil actual cargado: " + currentProfileName);
            }
        }
        else
        {
            currentProfileText.text = "Perfil actual: Ninguno";
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
                SwitchProfile(newProfileName);
                Debug.Log("Perfil creado: " + newProfileName);
                UpdateProfileDropdownOptions();
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


    public void SwitchProfile(string profileName)
    {
        // Cambiar al perfil seleccionado
        if (profiles.ContainsKey(profileName))
        {
            currentProfile = profiles[profileName];
            PlayerPrefs.SetString(currentProfileKey, currentProfile.profileName);
            currentProfileText.text = "Perfil actual: " + currentProfile.profileName;
            // Actualizar el cuadro de texto con el perfil actual
            Debug.Log("Perfil cambiado a: " + currentProfile.profileName);
        }
        else
        {
            Debug.LogWarning("No se encontró el perfil: " + profileName);
        }
    }


    private void SaveProfiles()
    {
        for (int i = 0; i < profiles.Count; i++)
        {
            string profileKey = "Profile_" + i.ToString();
            string profileData = PlayerPrefs.GetString(profileKey);
            Debug.Log("PlayerPrefs Key: " + profileKey + ", Value: " + profileData);
        }
        // Guardar perfiles en PlayerPrefs
        PlayerPrefs.SetInt("ProfileCount", profiles.Count);
        int index = 0;
        foreach (KeyValuePair<string, Profile> kvp in profiles)
        {
            string profileKey = "Profile_" + index.ToString();
            string profileData = JsonUtility.ToJson(kvp.Value);
            PlayerPrefs.SetString(profileKey, profileData);
            index++;
            
        }
        PlayerPrefs.Save();
        
    }


    public void DeleteProfile()
    {
        if (currentProfile != null)
        {
            string profileToDelete = currentProfile.profileName;
            profiles.Remove(profileToDelete);
            currentProfile = null;
            PlayerPrefs.DeleteKey(currentProfileKey);
            SaveProfiles();
            // Actualizar el cuadro de texto con un mensaje adecuado
            currentProfileText.text = "Perfil actual: Ninguno";
            Debug.Log("Perfil borrado.");
            UpdateProfileDropdownOptions();
            if(profileDropdown.options.Count > 0)
            {
                profileDropdown.value = 0;
                profileDropdown.RefreshShownValue();
                SwitchProfile(profileDropdown.options[0].text);
            }
        }
        else
        {
            Debug.LogWarning("No hay un perfil actual para borrar.");
        }
    }
    private void UpdateProfileDropdownOptions()
    {
        profileDropdown.ClearOptions();
        List<string> profileNames = new List<string>(profiles.Keys);
        profileDropdown.AddOptions(profileNames);
    }

    public void OnProfileDropdownValueChanged(int index)
    {
        string selectedProfileName = profileDropdown.options[index].text;
        SwitchProfile(selectedProfileName);
    }

}

[System.Serializable]
public class Profile
{
    public string profileName;
    // Agrega los datos que deseas guardar para cada perfil

    public Profile(string name)
    {
        profileName = name;
        // Inicializa los datos de perfil según tus necesidades
    }
}
