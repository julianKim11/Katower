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
    public List<Text> towerCountText;
    public List<Button> upgradeButtons;
    private int luchador;
    private int trampero;
    private int enamorado;

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

        UpdateTowerUI();
    }
    public void ActivateProfile()
    {
        //isProfileActive = true;
        UpdateUI();
    }
    public void DisableRollButton()
    {
        //isProfileActive = false;
        DisableUI();
    }
    public void ResetTowerCounts()
    {
       //towerCount.Clear();
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
        if (PlayerPrefs.HasKey("Luchador"))
        {
            luchador = PlayerPrefs.GetInt("Luchador");
            towerCountText[0].text = luchador.ToString();
        }
        else
        {
            towerCountText[0].text = "0";
        }

        if (PlayerPrefs.HasKey("Trampero"))
        {
            trampero = PlayerPrefs.GetInt("Trampero");
            towerCountText[1].text = trampero.ToString();
        }
        else
        {
            towerCountText[1].text = "0";
        }

        if (PlayerPrefs.HasKey("Enamorado"))
        {
            enamorado = PlayerPrefs.GetInt("Enamorado");
            towerCountText[2].text = enamorado.ToString();
        }
        else
        {
            towerCountText[2].text = "0";
        }
    }
    public void ResetTowerCount()
    {
        luchador = 0;
        trampero = 0;
        enamorado = 0;
    }
    public void RollGacha()
    {
        if (MainManager.Instance.Yarm > 0)
        {
            MainManager.Instance.Yarm--;

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
                    if (PlayerPrefs.HasKey(item.name))
                    {
                        int currentCount = PlayerPrefs.GetInt(item.name);
                        currentCount++;
                        PlayerPrefs.SetInt(item.name, currentCount);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(item.name, 1);
                    }

                    if (item.name == "Luchador")
                    {
                        luchador++;
                    }
                    else if (item.name == "Trampero")
                    {
                        trampero++;
                    }
                    else if (item.name == "Enamorado")
                    {
                        enamorado++;
                    }
                    Debug.Log("¡Has obtenido la torre: " + item.name + "!");
                    //MainManager.instance.tower = item.towerPrefab;
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