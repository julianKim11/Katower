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
        GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);
    }
    private void PriceCheck()
    {
        if(price <= GameManager.Instance.Currency)
        {
            GetComponent<Image>().color = Color.white;
            priceText.color = Color.black;
        }
        else
        {
            GetComponent<Image>().color = Color.grey;
            priceText.color = Color.grey;
        }
    }
}
