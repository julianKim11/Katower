using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Element { TIERRA, AGUA, VIENTO, NINGUNO}

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private string projectileType;
    [SerializeField] private float projectileSpeed;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Enemy target;
    [SerializeField] private int damage;
    [SerializeField] private float debuffDuration;
    [SerializeField] private float proc;
    public Element ElementType { get; protected set; }
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            this.damage = value;
        }
    }
    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
        set
        {
            this.projectileSpeed = value;
        }
    }
    public float DebuffDuration
    {
        get
        {
            return debuffDuration;
        }
        set
        {
            this.debuffDuration = value;
        }
    }
    public Enemy Target
    {
        get
        {
            return target;
        }
    }
    public float Proc
    {
        get
        {
            return proc;
        }
        set
        {
            this.proc = value;
        }
    }
    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] private float attackCooldown;
    private Queue<Enemy> enemies = new Queue<Enemy>();
    public float AttackCooldown
    {
        get
        {
            return attackCooldown;
        }
        set
        {
            this.attackCooldown = value;
        }
    }
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = transform.parent.GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();
        Debug.Log(target);
    }
    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
    private void Attack()
    {
        if(!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if(target == null && enemies.Count > 0 && enemies.Peek().IsActive)
        {
            target = enemies.Dequeue();
        }
        if(target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();
                myAnimator.SetTrigger("Attack");
                canAttack = false;
            }
        }
        if(target != null && !target.Alive || target != null && !target.IsActive)
        {
            target = null;
        }
    }
    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemies.Enqueue(other.GetComponent<Enemy>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            target = null;
        }
    }
    //public void ApplyDamageBuff(int amount)
    //{
    //    damage += amount;
    //}
    public abstract Debuff GetDebuff();

    public void ApplyDamageBuff(int amount)
    {
        damage += amount;
    }
    public void RemoveDamageBuff(int amount)
    {
        damage -= amount;
    }
}
