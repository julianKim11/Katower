using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    [SerializeField] private GameObject towerPrebaf;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;
    [SerializeField] private Text priceText;

    public GameObject TowerPrebaf { get => towerPrebaf; }
    public Sprite Sprite { get => sprite; }
    public int Price { get => price; }

    private void Start()
    {
        priceText.text = Price.ToString();
    }
}
