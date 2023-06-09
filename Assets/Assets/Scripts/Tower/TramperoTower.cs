using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramperoTower : Tower
{
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
        ElementType = Element.TIERRA;
    }
    public override Debuff GetDebuff()
    {
        return new TramperoDebuff(Target, trampDamage, tickTime, trampPrefab, DebuffDuration);
        //TramperoDebuff debuff = new TramperoDebuff(Target, trampDamage, tickTime, trampPrefab, DebuffDuration);
        //return debuff;
    }
}
