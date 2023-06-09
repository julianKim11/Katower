using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>
{
    public event CurrencyChanged Changed;
    public InGameMenu inGameMenu;
    public TowerBtn ClickedBtn { get; set; }
    //private int health = 100;
    //private int bossHealth = 550;
    private int currency;
    private int wave = 0;
    private int waveQ = 3;
    private int lives;
    private int enemyIndex;
    private bool completedWave5 = false;
    [SerializeField] private Text livesText;
    [SerializeField] private Text currencyText;
    [SerializeField] private GameObject waveBtn;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject panelShopBtn;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text waveText;
    private bool gameOver = false;
    private bool winScreen = false;
    public ObjectPool Pool { get; set; }
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject winScreenMenu;
    private Tower selectedTower;
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
            this.currencyText.text = value.ToString();
            
            OnCurrencyChanged();
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
            else if (completedWave5 && wave > 5)
            {
                WinScreen();
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
        Lives = 10;
        Currency = 1500;
    }
    private void Update()
    {
        //Debug.Log(wave);
        //HandleEscape();
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
    public void OnCurrencyChanged()
    {
        if(Changed != null)
        {
            Changed();
            Debug.Log("Currency Changed");
        }
    }
    public void SelectTower(Tower tower)
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = tower;
        selectedTower.Select();
    }
    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
    }
    //private void HandleEscape()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Hover.Instance.Deactivate();
    //        inGameMenu.ShowInGameMenu();
    //    }
    //}
    public void StartWave()
    {
        wave++;
        waveText.text = string.Format("Oleada: <color=lime>{0}</color>", wave);
        StartCoroutine(SpawnWave());
        waveBtn.SetActive(false);
        
        if(wave > 5 && Lives > 0)
        {
            completedWave5 = true;
        }
    }
    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        if(wave == 2)
        {
            waveQ = 5;
            enemyIndex = 0;
        }
        if(wave == 3)
        {
            waveQ = 8;
            enemyIndex = 0;
        }
        if (wave == 4)
        {
            waveQ = 10;
            enemyIndex = 0;
        }
        if (wave == 5)
        {
            waveQ = 1;
            enemyIndex = 1;
        }
        for (int i = 0; i < waveQ; i++)
        {
            //int enemyIndex = 0; //Random.Range(0, 3);

            string type = string.Empty;

            switch (enemyIndex)
            {
                case 0:
                    type = "Rat";
                    break;
                case 1:
                    type = "BossRat";
                    break;
            }

            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
            if(enemyIndex == 0)
            {
                enemy.Spawn();
            }
            if(enemyIndex == 1)
            {
                enemy.Spawn();
            }
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(3f);
        }
    }
    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);

        if (!waveActive && !gameOver)
        {
            if(wave == 5 && Lives > 0)
            {
                WinScreen();
                MainManager.instance.estambre += 10;
            }
            else
            {
                waveBtn.SetActive(true);
            }
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
    public void WinScreen()
    {
        if (!winScreen)
        {
            winScreen = true;
            winScreenMenu.SetActive(true);
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        LevelManager.Instance.GeneratePath();
        startBtn.SetActive(false);
        waveBtn.SetActive(true);
        panelShopBtn.SetActive(true);
        panel.SetActive(false);
    }
}