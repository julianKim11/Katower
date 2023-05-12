using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private string projectileType;
    [SerializeField] private float projectileSpeed;
    private Animator myAnimator;
    [SerializeField] private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
    }
    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }
    private SpriteRenderer mySpriteRenderer;
    private Enemy target;
    public Enemy Target
    {
        get
        {
            return target;
        }
    }
    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] private float attackCooldown;
    private Queue<Enemy> enemies = new Queue<Enemy>();
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
        if(target == null && enemies.Count > 0)
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
        else if (enemies.Count > 0)
        {
            target = enemies.Dequeue();
        }
        if(target != null && !target.Alive)
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
}
