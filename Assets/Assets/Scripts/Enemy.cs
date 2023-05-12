using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private Stack<Node> path;
    public Point GridPosition { get; set; }
    private Vector3 destination;
    public bool IsActive { get; set; }
    [SerializeField] private Stat health;
    private SpriteRenderer spriteRenderer;
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
        Move();
    }
    public void Spawn(int health)
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        this.health.Bar.Reset();
        this.health.MaxValue = health;
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
        IsActive = false;
        //GridPosition = LevelManager.Instance.BluePortal;
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        
    }
    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            health.CurrentValue -= damage;
            if(health.CurrentValue <= 0)
            {
                GameManager.Instance.Currency += 2;

                //myAnimator.SetTrigger("Die");

                GetComponent<SpriteRenderer>().sortingOrder--;
                Release();
            }
        }
    }
}