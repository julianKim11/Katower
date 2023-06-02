using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public List<GachaItem> gachaItems;
    public Image towerImage;
    public GameObject gachaPanel;
    public Button closeButton;
    public Button gachaButton;
    public Button rollButton;
    private bool isProfileActive = false;
    public Dictionary<string, int> towerCount = new Dictionary<string, int>();
    public List<Text> towerCountText;
    public List<Button> upgradeButtons;

    [System.Serializable]
    public class GachaItem
    {
        public string name;
        public float probability;
        public Sprite sprite;
        public GameObject towerPrefab;
        public bool isActive;
    }
    private void Start()
    {
        closeButton.onClick.AddListener(CloseGachaPanel);
        gachaButton.onClick.AddListener(OpenGachaPanel);
        rollButton.onClick.AddListener(RollGacha);
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int towerIndex = i; // Captura el índice de la torre actual en una variable local

            upgradeButtons[i].onClick.AddListener(() => UpgradeTowerStats(towerIndex));
        }
        //if (!isProfileActive)
        //{
        //    DisableRollButton();
        //    towerImage.gameObject.SetActive(false);
        //}
    }
    public void UpgradeTowerStats(int towerIndex)
    {
        Profile currentProfile = ProfileManager.GetCurrentProfile();

        switch (towerIndex)
        {
            case 0:
                if(currentProfile.luchadorCount >= 5)
                {
                    Debug.Log("Luchador");
                }
                break;
            case 1:
                if(currentProfile.tramperoCount >= 5)
                {
                    Debug.Log("Trampero");
                }
                break;
            case 2:
                if(currentProfile.enamoradoCount >= 5)
                {
                    Debug.Log("Enamorado");
                }
                break;

            default:
                Debug.LogWarning("Torre no reconocida");
                break;
        }
    }
    public void ActivateProfile()
    {
        isProfileActive = true;
        UpdateUI();
    }
    public void DisableRollButton()
    {
        isProfileActive = false;
        DisableUI();
    }
    public void ResetTowerCounts()
    {
       towerCount.Clear();
       UpdateTowerUI();
    }

    public void UpdateUI()
    {
        rollButton.interactable = true;
        towerImage.gameObject.SetActive(true);
    }
    private void DisableUI()
    {
        //rollButton.interactable = false;
        towerImage.gameObject.SetActive(false);
    }
    public void OpenGachaPanel()
    {
        gachaPanel.SetActive(true);
        towerImage.gameObject.SetActive(false);
    }

    public void CloseGachaPanel()
    {
        gachaPanel.SetActive(false);
    }

    public void UpdateTowerUI()
    {
        Profile currentProfile = ProfileManager.GetCurrentProfile();

        if (towerCount.ContainsKey("Luchador"))
        {
            currentProfile.luchadorCount = towerCount["Luchador"];
            towerCountText[0].text = currentProfile.luchadorCount.ToString();
        }
        else
        {
            towerCountText[0].text = "0";
        }

        if (towerCount.ContainsKey("Trampero"))
        {
            currentProfile.tramperoCount = towerCount["Trampero"];
            towerCountText[1].text = currentProfile.tramperoCount.ToString();
        }
        else
        {
            towerCountText[1].text = "0";
        }

        if (towerCount.ContainsKey("Enamorado"))
        {
            currentProfile.enamoradoCount = towerCount["Enamorado"];
            towerCountText[2].text = currentProfile.enamoradoCount.ToString();
        }
        else
        {
            towerCountText[2].text = "0";
        }
    }

    public void RollGacha()
    {
        if (!isProfileActive)
        {
            Debug.Log("No hay un perfil activo.");
            return;
        }

        Profile currentProfile = ProfileManager.GetCurrentProfile();
        if (currentProfile == null)
        {
            Debug.Log("No se ha seleccionado un perfil.");
            return;
        }

        if (MainManager.instance.estambre > 0)
        {
            MainManager.instance.estambre--;

            float randomValue = Random.value;
            float totalProbability = 0f;
            float cumulativeProbability = 0f;

            foreach (GachaItem item in gachaItems)
            {
                totalProbability += item.probability;
            }

            foreach (GachaItem item in gachaItems)
            {
                float itemRange = item.probability / totalProbability;
                cumulativeProbability += itemRange;

                if (randomValue <= cumulativeProbability)
                {
                    if (towerCount.ContainsKey(item.name))
                    {
                        towerCount[item.name]++;
                    }
                    else
                    {
                        towerCount[item.name] = 1;
                    }

                    if (item.name == "Luchador")
                    {
                        currentProfile.luchadorCount++;
                    }
                    else if (item.name == "Trampero")
                    {
                        currentProfile.tramperoCount++;
                    }
                    else if (item.name == "Enamorado")
                    {
                        currentProfile.enamoradoCount++;
                    }

                    //if (currentProfile != null && !currentProfile.acquiredTowers.Contains(item.name))
                    //{
                    //    currentProfile.acquiredTowers.Add(item.name);
                    //}

                    Debug.Log("¡Has obtenido la torre: " + item.name + "!");
                    MainManager.instance.tower = item.towerPrefab;
                    towerImage.sprite = item.sprite;
                    towerImage.gameObject.SetActive(true);
                    UpdateTowerUI();
                    break;
                }
            }
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }
}
