using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuchadorTower : Tower
{
    private void Start()
    {
        ElementType = Element.TORRETIERRA;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(250 ,50 ,0 ,0.5f),
            new TowerUpgrade(300 ,100 ,0 ,1f),
        };
    }
    public override Debuff GetDebuff()
    {
        //throw new System.NotImplementedException();
        return new LuchadorDebuff(Target);
    }
    public override string GetStats()
    {
        return string.Format("{0}{1}", "<size=20>Luchador</size>", base.GetStats());
    }
}
