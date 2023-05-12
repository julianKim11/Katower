using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Tower parent;
    private Enemy target;
    private void Update()
    {
        MoveToTarget();
    }
    public void Initialize(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }
    public void MoveToTarget()
    {
        if(target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);

            Vector2 dir = target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (!target.IsActive)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(target.gameObject == collision.gameObject)
            {
                target.TakeDamage(parent.Damage);
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }
}
