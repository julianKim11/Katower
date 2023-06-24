using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Element { TIERRA, AGUA, VIENTO, TORRETIERRA, TORREAGUA, TORREVIENTO }

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
    public int Level { get; protected set; }
    public int Price { get; set; }
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
    public TowerUpgrade NextUpgrade
    {
        get
        {
            if(Upgrades.Length > Level - 1)
            {
                return Upgrades[Level - 1];
            }
            return null;
        }
    }

    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] private float attackCooldown;

    public TowerUpgrade[] Upgrades { get; protected set; }
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
        Level = 1;
    }
    private void Update()
    {
        Attack();
        Debug.Log(target);
    }
    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        GameManager.Instance.UpdateUpgradeTip();
    }
    public virtual void Attack()
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
    public virtual string GetStats()
    {
        if(NextUpgrade != null)
        {
            return string.Format("\nNivel: {0} \nDanyo: {1} <color=#00ff00ff> +{3}</color> \nVelocidad de Ataque: {2} <color=#00ff00ff> -{4}</color>", Level, damage, attackCooldown, NextUpgrade.Damage, NextUpgrade.AttackSpeed);
        }
        return string.Format("\nNivel: {0} \nDanyo: {1} \nVel. de Ataque: {2}", Level, damage, attackCooldown);
    }
    public virtual void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }
    public virtual void Upgrade()
    {
        GameManager.Instance.Currency -= NextUpgrade.Price;
        Price += NextUpgrade.Price;
        this.damage += NextUpgrade.Damage;
        this.attackCooldown -= NextUpgrade.AttackSpeed;
        Level++;
        GameManager.Instance.UpdateUpgradeTip();
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
    public abstract Debuff GetDebuff();
}
