using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramperoDebuff : Debuff
{
    private float timeSinceTick;
    private float tickTime;
    private Tramp trampPrefab;
    private int trampDamage;
    public TramperoDebuff(Enemy target, int trampDamage, float tickTime, Tramp trampPrefab, float duration) : base(target, duration)
    {
        this.trampDamage = trampDamage;
        this.tickTime = tickTime;
        this.trampPrefab = trampPrefab;
    }
    public override void Update()
    {
        if(target != null)
        {
            Debug.Log("1");
            timeSinceTick += Time.deltaTime;
            if(timeSinceTick >= tickTime)
            {
                Debug.Log("2");
                timeSinceTick = 0;
                TrampOnTheGround();
            }
        }

        base.Update();
    }
    private void TrampOnTheGround()
    {
        Debug.Log("trampa");
        Tramp tmp = GameObject.Instantiate(trampPrefab, target.transform.position, Quaternion.identity);
        tmp.Damage = trampDamage;
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), tmp.GetComponent<Collider2D>());
    }
}
