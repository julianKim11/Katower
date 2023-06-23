using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>
{
    [Header("Upgrade Panel")]
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private Text sellText;
    [SerializeField] private Text upgradePrice;

    public TowerBtn ClickedBtn { get; set; }
    [SerializeField]
    private GameObject panelShopBtn;
    private Tower selectedTower;

    [Header("Menu")]
    public InGameMenu inGameMenu;

    public event CurrencyChanged Changed;
    public ObjectPool Pool { get; set; }
    private int currency;
    private int wave = 0;
    private int waveQ = 3;
    private int lives;
    private int enemyIndex;
    private bool waveCompleted = false;
    private int waveCount;
    [SerializeField] 
    private Text waveText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text currencyText;
    [SerializeField]
    private GameObject waveBtn;
    [SerializeField]
    private GameObject startBtn;

    [Header("Tooltip")]
    [SerializeField] private GameObject statPanel;
    [SerializeField] private Text statText;

    [Header("WinAndLose")]
    private bool gameOver = false;
    private bool winScreen = false;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject winScreenMenu;

    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialStartButton;
    [SerializeField] private GameObject tutorialTowerPanel;

    [Header("Enemy")]
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
            //else if (waveCompleted && wave == 3)
            //{
            //    WinScreen();
            //}
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
        sellText.text = "+ " + (selectedTower.Price / 2).ToString() + " $";
        UpgradePanel.SetActive(true);

    }
    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        UpgradePanel.SetActive(false);
        selectedTower = null;
    }
    private void HandleEscape()
    {
        //Salir del modo construccion
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
            inGameMenu.ShowInGameMenu();
        }
    }
    public void StartWave()
    {
        panelShopBtn.SetActive(false);
        tutorialTowerPanel.SetActive(false);
        wave++;
        waveText.text = string.Format("Oleada: <color=lime>{0}</color>", wave);
        waveBtn.SetActive(false);
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        //int enemyCount = 0;
        LevelManager.Instance.GeneratePath();
        if (wave == 1)
        {
            waveQ = 3;
            waveCount = 3;
            enemyIndex = 0;
        }
        if (wave == 2)
        {
            waveQ = 5;
            waveCount = 5;
            enemyIndex = 0;
        }
        if (wave == 3)
        {
            waveQ = 3;
            waveCount = 3;
            enemyIndex = 0;
        }
        int enemiesOfType0 = 1; // Variable para llevar la cuenta de los enemigos de tipo 0 generados

        for (int i = 0; i < waveQ; i++)
        {
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
            if(wave == 1 || wave == 2) 
            {
                enemy.Spawn();
                waveCount--;
            }
            if(wave == 3)
            {
                if (enemyIndex == 0 && enemiesOfType0 < 2) // Generar 2 enemigos de tipo 0
                {
                    enemiesOfType0++;
                    enemy.Spawn();
                }
                else if (enemyIndex == 1 || enemiesOfType0 >= 2) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                {
                    enemyIndex = 1;
                    enemy.Spawn();
                }
                waveCount--;
            }
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(2f);
        }
        if (waveCount == 0)
        {
            waveBtn.SetActive(true);
            panelShopBtn.SetActive(true);
        }
    }
    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        //Debug.Log("eliminado");
        
        if (/*!waveActive && !gameOver*/wave == 3 && Lives > 0 && waveCount == 0)
        {
            Debug.Log("Ganaste");
            WinScreen();
            MainManager.Instance.WinYarm(2);
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
        tutorialStartButton.SetActive(false);
        tutorialTowerPanel.SetActive(true);
    }
    public void ShowStats()
    {
        statPanel.SetActive(!statPanel.activeSelf);
    }
    public void ShowSelectedTowerStats()
    {
        statPanel.SetActive(!statPanel.activeSelf);
        UpdateUpgradeTip();
    }
    public void SetTooltipText(string txt)
    {
        statText.text = txt;
        //sizeText.text = txt;
    }
    public void UpdateUpgradeTip()
    {
        if(selectedTower != null)
        {
            sellText.text = "+ " + (selectedTower.Price / 2).ToString() + " $";
            SetTooltipText(selectedTower.GetStats());
            if(selectedTower.NextUpgrade != null)
            {
                upgradePrice.text = selectedTower.NextUpgrade.Price.ToString() + " $";
            }
            else
            {
                upgradePrice.text = string.Empty;
            }
        }
    }
    public void SellTower()
    {
        if(selectedTower != null)
        {
            Currency += selectedTower.Price / 2;

            selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;
            Destroy(selectedTower.transform.parent.gameObject);
            DeselectTower();
        }
    }
    public void UpgradeTower()
    {
        if(selectedTower != null)
        {
            if(selectedTower.Level <= selectedTower.Upgrades.Length && Currency >= selectedTower.NextUpgrade.Price)
            {
                selectedTower.Upgrade();
            }
        }
    }
}