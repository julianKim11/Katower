using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade
{
    public int Price { get; private set; }
    public int Damage { get; private set; }
    public float DebuffDuration { get; private set; }
    public float AttackSpeed { get; private set; }
    public float TrampDamage { get; private set; }
    public float PlusDamage { get; private set; }

    public TowerUpgrade(int price, int damage, float attackSpeed)
    {
        this.Price = price;
        this.Damage = damage;
        this.AttackSpeed = attackSpeed;
    }
    public TowerUpgrade(int price, int damage, float debuffDuration, float attackSpeed, float trampDamage)
    {
        this.Price = price;
        this.Damage = damage;
        this.DebuffDuration = debuffDuration;
        this.AttackSpeed = attackSpeed;
        this.TrampDamage = trampDamage;
    }
    public TowerUpgrade(int price, int damage,float attackSpeed, float plusDamage)
    {
        this.Price = price;
        this.Damage = damage;
        this.AttackSpeed = attackSpeed;
        this.PlusDamage = plusDamage;
    }
}
