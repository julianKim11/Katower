using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    public List<GachaItem> gachaItems;

    [System.Serializable]
    public class GachaItem
    {
        public string name;
        public int rarity;
        public Sprite sprite;
    }

    public GachaItem GetRandomGachaItem()
    {
        int totalRarity = 0;
        foreach (GachaItem item in gachaItems)
        {
            totalRarity += item.rarity;
        }

        int randomValue = Random.Range(0, totalRarity);

        int cumulativeRarity = 0;
        foreach (GachaItem item in gachaItems)
        {
            cumulativeRarity += item.rarity;
            if (cumulativeRarity > randomValue)
            {
                return item;
            }
        }

        return null;
    }
}
