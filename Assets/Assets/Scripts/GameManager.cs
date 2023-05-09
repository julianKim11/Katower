using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    private int currency;
    private int wave = 0;
    private int lives;
    [SerializeField] private Text livesText;
    [SerializeField] private Text currencyText;
    [SerializeField] private GameObject waveBtn;
    [SerializeField] private Text waveText;
    private bool gameOver = false;
    public ObjectPool Pool { get; set; }
    [SerializeField] private GameObject gameOverMenu;
    List<Enemy> activeEnemies = new List<Enemy>();
    public bool waveActive
    {
        get
        {
            return activeEnemies.Count > 0;
        }
    }
    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            this.currency = value;
            this.currencyText.text = "<color=lime>$</color>" + value.ToString();
        }
    }
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            this.lives = value;
            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }
            livesText.text = lives.ToString();
        }
    }
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }
    private void Start()
    {
        Lives = 3;
        Currency = 100;
    }
    private void Update()
    {
        HandleEscape();
    }
    public void PickTower(TowerBtn towerBtn)
    {
        if(Currency >= towerBtn.Price && !waveActive)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }
    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
        }    
    }
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
    public void StartWave()
    {
        wave++;
        waveText.text = string.Format("Wave: <color=lime>{0}</color>", wave);
        StartCoroutine(SpawnWave());
        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        for (int i = 0; i < wave; i++)
        {
            int enemyIndex = 0; //Random.Range(0, 3);

            string type = string.Empty;

            switch (enemyIndex)
            {
                case 0:
                    type = "Rat";
                    break;

            }

            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
            enemy.Spawn();
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(2.5f);
        }
    }
    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (!waveActive && !gameOver)
        {
            waveBtn.SetActive(true);
        }
    }
    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}