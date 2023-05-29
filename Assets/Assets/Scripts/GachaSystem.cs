using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public List<GachaItem> gachaItems;
    public int moneda = 0; // Variable para controlar las monedas disponibles
    public Button rollButton; // Referencia al botón "Roll" en la interfaz de usuario

    [System.Serializable]
    public class GachaItem
    {
        public string name;
        public float probability;
        public Sprite sprite;
    }

    private void Start()
    {
        rollButton.onClick.AddListener(RollGacha); // Asignar la función RollGacha() al evento onClick del botón "Roll"
    }

    public void RollGacha()
    {
        if (moneda > 0) // Verificar si hay monedas disponibles para gastar
        {
            moneda--; // Gastar una moneda

            float randomValue = Random.value; // Generar un valor aleatorio entre 0 y 1

            float cumulativeProbability = 0f;
            foreach (GachaItem item in gachaItems)
            {
                cumulativeProbability += item.probability;

                if (randomValue <= cumulativeProbability)
                {
                    Debug.Log("¡Has obtenido la torre: " + item.name + "!");
                    // Aquí puedes realizar la acción correspondiente a obtener la torre, como instanciar el objeto en el juego, mostrar su imagen, etc.
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

