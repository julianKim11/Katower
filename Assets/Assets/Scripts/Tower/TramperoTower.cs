using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramperoTower : Tower
{
    [SerializeField] private GameObject towerPrebaf;
    [SerializeField] private float tickTime;
    [SerializeField] private Tramp trampPrefab;
    [SerializeField] private int trampDamage;
    public int TrampDamage
    {
        get
        {
            return trampDamage;
        }
    }
    public float TickTime
    {
        get
        {
            return tickTime;
        }
    }
    private void Start()
    {
        ElementType = Element.TORRETIERRA;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(250 ,50 ,0 ,0.25f,20),
            new TowerUpgrade(300 ,50 ,0 ,0.25f,20),
        };
    }
    public override Debuff GetDebuff()
    {
        return new TramperoDebuff(Target, trampDamage, tickTime, trampPrefab, DebuffDuration);
        //TramperoDebuff debuff = new TramperoDebuff(Target, trampDamage, tickTime, trampPrefab, DebuffDuration);
        //return debuff;
    }
    public override string GetStats()
    {
        TramperoTower trampero = towerPrebaf.GetComponentInChildren<TramperoTower>();
        return string.Format("{0}{1}{2}{3}", "<size=20>Trampero</size>", base.GetStats(), "\nHabilidad: Deja trampas", "\nDanyo de trampa: "+trampero.trampDamage);
    }
}
