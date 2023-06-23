using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamoradoTower : Tower
{
    [SerializeField] private int buffDamageAmount;
    private void Start()
    {
        ElementType = Element.TORRETIERRA;
    }
    public int BuffDamageAmount
    {
        get { return BuffDamageAmount; }
        set { BuffDamageAmount = value; }
    }
    public override Debuff GetDebuff()
    {
        return new EnamoradoDebuff(Target);

    }
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTRO E");
        Tower tower = collision.GetComponent<Tower>();
        if (collision.tag == "Torre")
        {
            tower.ApplyDamageBuff(BuffDamageAmount);
        }

    }
    private new void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("SALIO E");
        Tower tower = collision.GetComponent<Tower>();
        
        if (collision.tag == "Torre")
        {
            tower.RemoveDamageBuff(BuffDamageAmount);
        }
    }
    
}
