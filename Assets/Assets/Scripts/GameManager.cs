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
    private int sceneNumber;
    public TowerBtn ClickedBtn { get; set; }
    [SerializeField]
    private GameObject panelShopBtn;
    [SerializeField]
    private GameObject panelOpenClose;
    private Tower selectedTower;

    [Header("Menu")]
    public InGameMenu inGameMenu;

    public event CurrencyChanged Changed;
    public ObjectPool Pool { get; set; }
    private int currency;
    private int wave = 1;
    private int waveQ;
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

    [SerializeField] private LevelManager levelManager;

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

    private int moneyPerWave = 500;
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
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        Lives = 10;
        Currency = 1500;
    }
    private void Update()
    {
        Debug.Log(wave);
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
        sellText.text = "+" + (selectedTower.Price / 2).ToString() + "$";
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
        if(sceneNumber == 1)
        {
            tutorialTowerPanel.SetActive(false);
            waveText.text = string.Format("Oleada: <color=lime>{0}</color> de <color=lime>3</color>", wave);
        }
        else
        {
            waveText.text = string.Format("Oleada: <color=lime>{0}</color> de <color=lime>15</color>", wave);
        }
        wave++;
        waveBtn.SetActive(false);
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        //int enemyCount = 0;
        LevelManager.Instance.GeneratePath();
        switch (sceneNumber)
        {
            case 1:
                if (sceneNumber == 1)
                {
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
                }
                break;
            case 2:
                if (sceneNumber == 2)
                {
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
                        waveQ = 7;
                        waveCount = 7;
                        enemyIndex = 0;
                    }
                    if (wave == 4)
                    {
                        waveQ = 6;
                        waveCount = 6;
                        enemyIndex = 0;
                    }
                    if (wave == 5)
                    {
                        waveQ = 3;
                        waveCount = 3;
                        enemyIndex = 0;
                    }
                    if (wave == 6)
                    {
                        waveQ = 5;
                        waveCount = 5;
                        enemyIndex = 0;
                    }
                    if (wave == 7)
                    {
                        waveQ = 6;
                        waveCount = 6;
                        enemyIndex = 0;
                    }
                    if (wave == 8)
                    {
                        waveQ = 3;
                        waveCount = 3;
                        enemyIndex = 3;
                    }
                    if (wave == 9)
                    {
                        waveQ = 12;
                        waveCount = 12;
                        enemyIndex = 0;
                    }
                    if (wave == 10)
                    {
                        waveQ = 16;
                        waveCount = 16;
                        enemyIndex = 0;
                    }
                    if (wave == 11)
                    {
                        waveQ = 12;
                        waveCount = 12;
                        enemyIndex = 3;
                    }
                    if (wave == 12)
                    {
                        waveQ = 16;
                        waveCount = 16;
                        enemyIndex = 2;
                    }
                    if (wave == 13)
                    {
                        waveQ = 5;
                        waveCount = 5;
                        enemyIndex = 1;
                    }
                    if (wave == 14)
                    {
                        waveQ = 7;
                        waveCount = 7;
                        enemyIndex = 3;
                    }
                    if (wave == 15)
                    {
                        waveQ = 20;
                        waveCount = 20;
                        enemyIndex = 3;
                    }
                    //if (wave == 16)
                    //{
                    //    waveQ = 21;
                    //    waveCount = 21;
                    //    enemyIndex = 3;
                    //}
                    //if (wave == 17)
                    //{
                    //    waveQ = 30;
                    //    waveCount = 30;
                    //    enemyIndex = 2;
                    //}
                    //if (wave == 18)
                    //{
                    //    waveQ = 10;
                    //    waveCount = 10;
                    //    enemyIndex = 2;
                    //}
                    //if (wave == 19)
                    //{
                    //    waveQ = 30;
                    //    waveCount = 30;
                    //    enemyIndex = 3;
                    //}
                    //if (wave == 20)
                    //{
                    //    waveQ = 10;
                    //    waveCount = 10;
                    //    enemyIndex = 1;
                    //}
                }
                break;
            case 3:
                if (sceneNumber == 3)
                {
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
                        waveQ = 7;
                        waveCount = 7;
                        enemyIndex = 0;
                    }
                    if (wave == 4)
                    {
                        waveQ = 6;
                        waveCount = 6;
                        enemyIndex = 0;
                    }
                    if (wave == 5)
                    {
                        waveQ = 3;
                        waveCount = 3;
                        enemyIndex = 0;
                    }
                    if (wave == 6)
                    {
                        waveQ = 5;
                        waveCount = 5;
                        enemyIndex = 0;
                    }
                    if (wave == 7)
                    {
                        waveQ = 6;
                        waveCount = 6;
                        enemyIndex = 0;
                    }
                    if (wave == 8)
                    {
                        waveQ = 3;
                        waveCount = 3;
                        enemyIndex = 3;
                    }
                    if (wave == 9)
                    {
                        waveQ = 12;
                        waveCount = 12;
                        enemyIndex = 0;
                    }
                    if (wave == 10)
                    {
                        waveQ = 16;
                        waveCount = 16;
                        enemyIndex = 0;
                    }
                    if (wave == 11)
                    {
                        waveQ = 12;
                        waveCount = 12;
                        enemyIndex = 3;
                    }
                    if (wave == 12)
                    {
                        waveQ = 16;
                        waveCount = 16;
                        enemyIndex = 2;
                    }
                    if (wave == 13)
                    {
                        waveQ = 4;
                        waveCount = 4;
                        enemyIndex = 1;
                    }
                    if (wave == 14)
                    {
                        waveQ = 7;
                        waveCount = 7;
                        enemyIndex = 3;
                    }
                    if (wave == 15)
                    {
                        waveQ = 20;
                        waveCount = 20;
                        enemyIndex = 3;
                    }
                    //if (wave == 16)
                    //{
                    //    waveQ = 21;
                    //    waveCount = 21;
                    //    enemyIndex = 3;
                    //}
                    //if (wave == 17)
                    //{
                    //    waveQ = 30;
                    //    waveCount = 30;
                    //    enemyIndex = 2;
                    //}
                    //if (wave == 18)
                    //{
                    //    waveQ = 10;
                    //    waveCount = 10;
                    //    enemyIndex = 2;
                    //}
                    //if (wave == 19)
                    //{
                    //    waveQ = 30;
                    //    waveCount = 30;
                    //    enemyIndex = 3;
                    //}
                    //if (wave == 20)
                    //{
                    //    waveQ = 10;
                    //    waveCount = 10;
                    //    enemyIndex = 1;
                    //}
                }
                break;
            case 4:
                if (sceneNumber == 4)
                {
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
                        waveQ = 7;
                        waveCount = 7;
                        enemyIndex = 0;
                    }
                    if (wave == 4)
                    {
                        waveQ = 6;
                        waveCount = 6;
                        enemyIndex = 0;
                    }
                    if (wave == 5)
                    {
                        waveQ = 3;
                        waveCount = 3;
                        enemyIndex = 0;
                    }
                    if (wave == 6)
                    {
                        waveQ = 5;
                        waveCount = 5;
                        enemyIndex = 0;
                    }
                    if (wave == 7)
                    {
                        waveQ = 6;
                        waveCount = 6;
                        enemyIndex = 0;
                    }
                    if (wave == 8)
                    {
                        waveQ = 3;
                        waveCount = 3;
                        enemyIndex = 3;
                    }
                    if (wave == 9)
                    {
                        waveQ = 12;
                        waveCount = 12;
                        enemyIndex = 0;
                    }
                    if (wave == 10)
                    {
                        waveQ = 16;
                        waveCount = 16;
                        enemyIndex = 0;
                    }
                    if (wave == 11)
                    {
                        waveQ = 12;
                        waveCount = 12;
                        enemyIndex = 3;
                    }
                    if (wave == 12)
                    {
                        waveQ = 16;
                        waveCount = 16;
                        enemyIndex = 2;
                    }
                    if (wave == 13)
                    {
                        waveQ = 4;
                        waveCount = 4;
                        enemyIndex = 1;
                    }
                    if (wave == 14)
                    {
                        waveQ = 7;
                        waveCount = 7;
                        enemyIndex = 3;
                    }
                    if (wave == 15)
                    {
                        waveQ = 20;
                        waveCount = 20;
                        enemyIndex = 3;
                    }
                    //if (wave == 16)
                    //{
                    //    waveQ = 21;
                    //    waveCount = 21;
                    //    enemyIndex = 3;
                    //}
                    //if (wave == 17)
                    //{
                    //    waveQ = 30;
                    //    waveCount = 30;
                    //    enemyIndex = 2;
                    //}
                    //if (wave == 18)
                    //{
                    //    waveQ = 10;
                    //    waveCount = 10;
                    //    enemyIndex = 2;
                    //}
                    //if (wave == 19)
                    //{
                    //    waveQ = 30;
                    //    waveCount = 30;
                    //    enemyIndex = 3;
                    //}
                    //if (wave == 20)
                    //{
                    //    waveQ = 10;
                    //    waveCount = 10;
                    //    enemyIndex = 1;
                    //}
                }
                break;
        }
        int enemiesOfType0 = 0; // Variable para llevar la cuenta de los enemigos de tipo 0 generados

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
                case 2:
                    type = "RatAmarilla";
                    break;
                case 3:
                    type = "RatAzul";
                    break;
                case 4:
                    type = "RatAzul1";
                    break;
            }

            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
            switch (sceneNumber)
            {
                case 1:
                    if (sceneNumber == 1)
                    {
                        if (wave == 1 || wave == 2)
                        {
                            enemy.Spawn();
                            waveCount--;
                        }
                        if (wave == 3)
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
                    }
                    break;
                case 2:
                    if (sceneNumber == 2)
                    {
                        if (wave == 1 || wave == 2 || wave == 8 || wave == 9 || wave == 10 || wave == 13 || wave == 14 || wave == 15)
                        {
                            enemy.Spawn();
                            waveCount--;
                        }
                        if (wave == 3)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 5) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 2 || enemiesOfType0 >= 2) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 2;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 4)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 3) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 2 || enemiesOfType0 >= 3) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 2;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 5)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 2) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 1 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 6)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 4) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 3 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 3;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 7)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 5) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 11 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 11)
                        {
                            if (enemyIndex == 3 && enemiesOfType0 < 10) // Generar 2 enemigos de tipo 0
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
                        if (wave == 12)
                        {
                            if (enemyIndex == 2 && enemiesOfType0 < 15) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 1 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                    }
                    break;
                case 3:
                    if (sceneNumber == 3)
                    {
                        if (wave == 1 || wave == 2 || wave == 8 || wave == 9 || wave == 10 || wave == 13 || wave == 14 || wave == 15)
                        {
                            enemy.Spawn();
                            waveCount--;
                        }
                        if (wave == 3)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 5) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 2 || enemiesOfType0 >= 2) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 2;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 4)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 3) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 2 || enemiesOfType0 >= 3) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 2;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 5)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 2) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 1 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 6)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 4) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 3 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 3;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 7)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 5) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 11 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 11)
                        {
                            if (enemyIndex == 3 && enemiesOfType0 < 10) // Generar 2 enemigos de tipo 0
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
                        if (wave == 12)
                        {
                            if (enemyIndex == 2 && enemiesOfType0 < 15) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 1 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                    }
                    break;
                case 4:
                    if (sceneNumber == 4)
                    {
                        if (wave == 1 || wave == 2 || wave == 8 || wave == 9 || wave == 10 || wave == 13 || wave == 14 || wave == 15)
                        {
                            enemy.Spawn();
                            waveCount--;
                        }
                        if (wave == 3)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 5) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 2 || enemiesOfType0 >= 2) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 2;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 4)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 3) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 2 || enemiesOfType0 >= 3) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 2;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 5)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 2) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 1 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 6)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 4) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 3 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 3;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 7)
                        {
                            if (enemyIndex == 0 && enemiesOfType0 < 5) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 11 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                        if (wave == 11)
                        {
                            if (enemyIndex == 3 && enemiesOfType0 < 10) // Generar 2 enemigos de tipo 0
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
                        if (wave == 12)
                        {
                            if (enemyIndex == 2 && enemiesOfType0 < 15) // Generar 2 enemigos de tipo 0
                            {
                                enemiesOfType0++;
                                enemy.Spawn();
                            }
                            else if (enemyIndex == 1 || enemiesOfType0 >= 1) // Cambiar a enemyIndex = 1 después de generar los 2 enemigos de tipo 0
                            {
                                enemyIndex = 1;
                                enemy.Spawn();
                            }
                            waveCount--;
                        }
                    }
                    break;
            }
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(2.5f);
        if (waveCount == 0)
        {
            waveBtn.SetActive(true);
            Currency += moneyPerWave;
        }
    }
    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        
        if (wave == 3 && Lives > 0 && !waveActive && sceneNumber == 1)
        {
            Debug.Log("Ganaste");
            WinScreen();
            MainManager.Instance.WinYarm(2);
        }
        if (wave == 20 && Lives > 0 && waveCount == 0 && sceneNumber == 2)
        {
            Debug.Log("Ganaste");
            WinScreen();
            MainManager.Instance.WinYarm(2);
        }
        if (wave == 20 && Lives > 0 && waveCount == 0 && sceneNumber == 3)
        {
            Debug.Log("Ganaste");
            WinScreen();
            MainManager.Instance.WinYarm(2);
        }
        if (wave == 20 && Lives > 0 && waveCount == 0 && sceneNumber == 4)
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
            //levelManager.ResetLevelManager();
        }
    }
    public void WinScreen()
    {
        if (!winScreen)
        {
            winScreen = true;
            winScreenMenu.SetActive(true);
            //levelManager.ResetLevelManager();
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
        //levelManager.ResetLevelManager();
    }
    public void StartGame()
    {
        panelShopBtn.SetActive(true);
        panelOpenClose.SetActive(false);
        LevelManager.Instance.GeneratePath();
        startBtn.SetActive(false);
        waveBtn.SetActive(true);
        if(sceneNumber == 1)
        {
            tutorialStartButton.SetActive(false);
            tutorialTowerPanel.SetActive(true);
        }
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
            sellText.text = "+" + (selectedTower.Price / 2).ToString() + "$";
            SetTooltipText(selectedTower.GetStats());
            if(selectedTower.NextUpgrade != null)
            {
                upgradePrice.text = selectedTower.NextUpgrade.Price.ToString() + "$";
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
    public void StorePanelShopOpen()
    {
        panelShopBtn.SetActive(true);
        panelOpenClose.SetActive(false);
    }
    public void StorePanelShopClose()
    {
        panelShopBtn.SetActive(false);
        panelOpenClose.SetActive(true);
    }
}