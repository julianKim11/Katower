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
    public Button rollButton; // Referencia al botón "Roll" en la interfaz de usuario
    private bool isProfileActive = false;

    [System.Serializable]
    public class GachaItem
    {
        public string name;
        public float probability;
        public Sprite sprite;
        public GameObject towerPrefab;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(CloseGachaPanel);
        gachaButton.onClick.AddListener(OpenGachaPanel);
        towerImage.gameObject.SetActive(false);
        rollButton.onClick.AddListener(RollGacha); // Asignar la función RollGacha() al evento onClick del botón "Roll"
        if (!isProfileActive)
        {
            rollButton.interactable = false;
            towerImage.gameObject.SetActive(false);
        }
    }
    public void ActivateProfile()
    {
        Debug.Log("ho");
        isProfileActive = true;
        UpdateUI();
    }
    private void UpdateUI()
    {
        rollButton.interactable = true;
        towerImage.gameObject.SetActive(true);
    }
    public void OpenGachaPanel()
    {
        gachaPanel.SetActive(true);
    }
    public void CloseGachaPanel()
    {
        gachaPanel.SetActive(false);
    }
    public void RollGacha()
    {
        if (!isProfileActive)
        {
            Debug.Log("No hay un perfil activo.");
            return;
        }

        if (MainManager.instance.moneda > 0) // Verificar si hay monedas disponibles para gastar
        {
            MainManager.instance.moneda--; // Gastar una moneda

            float randomValue = Random.value; // Generar un valor aleatorio entre 0 y 1
            float totalProbability = 0f;
            float cumulativeProbability = 0f;

            foreach(GachaItem item in gachaItems)
            {
                totalProbability += item.probability;
            }

            foreach (GachaItem item in gachaItems)
            {
                float itemRange = item.probability / totalProbability;
                cumulativeProbability += itemRange;

                if (randomValue <= cumulativeProbability)
                {
                    Debug.Log("¡Has obtenido la torre: " + item.name + "!");
                    // Aquí puedes realizar la acción correspondiente a obtener la torre, como instanciar el objeto en el juego, mostrar su imagen, etc.
                    MainManager.instance.tower = item.towerPrefab;
                    towerImage.sprite = item.sprite;
                    towerImage.gameObject.SetActive(true);
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