using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private Stat health;
    [SerializeField] private float maxHealth;

    [Header("Position")]
    private Stack<Node> path;
    public Point GridPosition { get; set; }
    private Vector3 destination;
    private SpriteRenderer spriteRenderer;

    [Header("Enemy")]
    [SerializeField] private float speed;
    [SerializeField] private Element elementType;
    public bool IsActive { get; set; }
    [SerializeField] private int fishDropOnKill;
    
    [Header("Debuffs")]
    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> newDebuffs = new List<Debuff>();
    public Element ElementType
    {
        get
        {
            return elementType;
        }
    }
    public bool Alive
    {
        get { return health.CurrentValue > 0; }
    }
    private void Awake()
    {
        health.Initialize();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        HandleDebuffs();
        Move();
    }
    public void Spawn()
    {
        debuffs.Clear();
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        this.health.Bar.Reset();
        this.health.MaxValue = maxHealth;
        this.health.CurrentValue = this.health.MaxValue;

        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1), false));

        SetPath(LevelManager.Instance.Path);
    }
    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {
        float progress = 0;

        while(progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        transform.localScale = to;

        IsActive = true;

        if (remove)
        {
            Release();
        }
    }
    private void Move()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }
    public void SetPath(Stack<Node> newPath)
    {
        if(newPath != null)
        {
            this.path = newPath;
            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "RedPortal")
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));

            GameManager.Instance.Lives--;
        }
        if(other.tag == "Tile")
        {
            spriteRenderer.sortingOrder = other.GetComponent<TileScript>().GridPosition.y;
        }
    }
    private void Release()
    {
        debuffs.Clear();
        IsActive = false;
        //GridPosition = LevelManager.Instance.BluePortal;
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        
    }
    public void TakeDamage(int damage, Element damageSource)
    {
        if (IsActive)
        {
            float damageMultiplier = 1f;

            if(damageSource == Element.TORRETIERRA && elementType == Element.VIENTO)
            {
                //TIERRA VS VIENTO
                damageMultiplier = 1.2f;
            } else if(damageSource == Element.TORRETIERRA && elementType == Element.AGUA)
            {
                //TIERRA VS AGUA
                damageMultiplier = 0.8f;
            }
            else if (damageSource == Element.TORREVIENTO && elementType == Element.AGUA)
            {
                //VIENTO VS AGUA
                damageMultiplier = 1.2f;
            }
            else if (damageSource == Element.TORREVIENTO && elementType == Element.TIERRA)
            {
                //VIENTO VS TIERRA
                damageMultiplier = 0.8f;
            }
            else if (damageSource == Element.TORREAGUA && elementType == Element.TIERRA)
            {
                //AGUA VS TIERRA
                damageMultiplier = 1.2f;
            }
            else if (damageSource == Element.TORREAGUA && elementType == Element.VIENTO)
            {
                //AGUA VS VIENTO
                damageMultiplier = 0.8f;
            }

            damage = Mathf.RoundToInt(damage * damageMultiplier);

            health.CurrentValue -= damage;

            if(health.CurrentValue <= 0)
            {
                GameManager.Instance.Currency += fishDropOnKill;

                //myAnimator.SetTrigger("Die");

                GetComponent<SpriteRenderer>().sortingOrder--;
                Release();
            }
        }
    }
    public void AddDebuf(Debuff debuff)
    {
        if(!debuffs.Exists(x => x.GetType() == debuff.GetType()))
        {
            Debug.Log("AddDebuf");
            newDebuffs.Add(debuff);
        }
    }
    public void RemoveDebuff(Debuff debuff)
    {
        debuffsToRemove.Add(debuff);
        Debug.Log("RemoveDebuf");
    }
    private void HandleDebuffs()
    {
        if(newDebuffs.Count > 0)
        {
            Debug.Log("NewDebuf");
            debuffs.AddRange(newDebuffs);
            newDebuffs.Clear();
        }

        foreach (Debuff debuff in debuffsToRemove)
        {
            debuffs.Remove(debuff);
        }

        debuffsToRemove.Clear();

        foreach (Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }
}