using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamoradoTower : Tower
{
    //public static event System.Action<int> OnEnamoradoTowerPlaced;
    //[SerializeField] private int buffDamageAmount;
    private int attackCount;
    private void Start()
    {
        ElementType = Element.TORRETIERRA;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(250, 5, 0.25f, 1),
            new TowerUpgrade(250, 5, 0.25f, 1),
        };
        //if(OnEnamoradoTowerPlaced != null)
        //    OnEnamoradoTowerPlaced(buffDamageAmount);
        attackCount = 1;
    }
    public override void Attack()
    {
        Debug.Log(attackCount);
        base.Attack();
        if (attackCount % 5 == 0)
        {
            Damage += 5;
            attackCount = 1;
        }
    }
    public override void Shoot()
    {
        base.Shoot();
        attackCount++;
    }
    public override Debuff GetDebuff()
    {
        return new EnamoradoDebuff(Target);
    }
    public override string GetStats()
    {
        return string.Format("{0}{1}", "<size=20>Enamorado</size>", base.GetStats());
    }
}
