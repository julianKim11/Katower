using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamoradoTower : Tower
{
    [SerializeField] private int BuffDamageAmount;
    private void Start()
    {
        ElementType = Element.ENAMORADO;
    }
    public override Debuff GetDebuff()
    {
        return new EnamoradoDebuff(Target);

    }
}
