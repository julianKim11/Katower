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
    public void ShowInfo(string type)
    {
        string tooltip = string.Empty;

        switch (type)
        {
            case "Luchador":
                LuchadorTower luchador = towerPrebaf.GetComponentInChildren<LuchadorTower>();
                tooltip = string.Format("Luchador\nDanyo: {0} \nVel. de ataque: {1}",luchador.Damage, luchador.AttackCooldown);
                break;
            case "Trampero":
                TramperoTower trampero = towerPrebaf.GetComponentInChildren<TramperoTower>();
                tooltip = string.Format("Trampero\nDanyo: {0} \nVel. de ataque: {1}\nHabilidad: Deja trampas\nDanyo de trampa: {2}", trampero.Damage, trampero.AttackCooldown, trampero.TrampDamage);
                break;
            case "Enamorado":
                EnamoradoTower enamorado = towerPrebaf.GetComponentInChildren<EnamoradoTower>();
                tooltip = string.Format("Enamorado\nDanyo: {0} \nVel. de ataque: {1}\nHabilidad: Cada 5 disparos \naumenta en 5 tu danyo.", enamorado.Damage, enamorado.AttackCooldown);
                break;

        }

        GameManager.Instance.SetTooltipText(tooltip);

        GameManager.Instance.ShowStats();
    }
}
