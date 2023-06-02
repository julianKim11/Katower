using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuchadorTower : Tower
{
    private void Start()
    {
        ElementType = Element.LUCHADOR;
    }
    public override Debuff GetDebuff()
    {
        //throw new System.NotImplementedException();
        return new LuchadorDebuff(Target);
    }
}
