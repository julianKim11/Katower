using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Transform map;
    private Point blueSpawn, redSpawn;
    public Spawn BluePortal { get; set; }
    [SerializeField] private GameObject bluePortalPrefab;
    [SerializeField] private GameObject redPortalPrefab;
    private Point mapSize;
    private Stack<Node> path;
    private int sceneNumber;
    //[SerializeField] private GameObject towerPrefab;
    //public static Stack<Node> fixedPath = new Stack<Node>();
    public Stack<Node> Path
    {
        get
        {
            if(path == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
    }
    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        CreateLevel();
    }
    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();

        mapSize = new Point(mapData[0].ToCharArray().Length, mapData.Length);

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }
        maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        float tileSize = TileSize - 0.01f;

        //Element towerElementType = GetTowerElementType(tileType);

        //newTile.ElementType = towerElementType;

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + tileSize * x, worldStart.y - tileSize * y, 0), map);
    }
    //private Element GetTowerElementType(string tileType)
    //{
    //    // Mapear el tipo de elemento según el tipo de tile
    //    switch (tileType)
    //    {
    //        case "0": // Tile de tipo TIERRA
    //            return Element.TORRETIERRA;
    //        case "1": // Tile de tipo AGUA
    //            return Element.TORREAGUA;
    //        case "2": // Tile de tipo VIENTO
    //            return Element.TORREVIENTO;
    //        default:
    //            return Element.TORRETIERRA; // Valor predeterminado
    //    }
    //}
    //private void PlaceTower(TileScript tile, Element towerElementType)
    //{
    //    if (tile != null && tile.IsEmpty && !tile.HasTower && tile.ElementType == towerElementType)
    //    {
    //        // Crear y colocar la torre en la posición de la tile
    //        GameObject towerObject = Instantiate(towerPrefab, tile.transform.position, Quaternion.identity);
    //        Tower newTower = towerObject.GetComponent<Tower>();
    //        tile.HasTower = true;

    //        // Configurar el tipo de elemento de la torre
    //        newTower.ElementType = towerElementType;
    //    }
    //}
    private string[] ReadLevelText()
    {
        string mapa = "";

        if(sceneNumber == 1)
        {
            mapa = "Level1-1";
        }
        if(sceneNumber == 2)
        {
            mapa = "Level1-2";
        }
        if (sceneNumber == 3)
        {
            mapa = "Level1-3";
        }
        if (sceneNumber == 4)
        {
            mapa = "Level1-4";
        }

        TextAsset bindData = Resources.Load(mapa) as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
    private void SpawnPortals()
    {
        if(sceneNumber == 1)
        {
            blueSpawn = new Point(2, 5);
            redSpawn = new Point(18, 6);//18,2 -2, 5
        }
        if (sceneNumber == 2)
        {
            blueSpawn = new Point(2, 1);
            redSpawn = new Point(17, 17);//18,2 -2, 5
        }
        if (sceneNumber == 3)
        {
            blueSpawn = new Point(2, 6);
            redSpawn = new Point(18, 14);//18,2 -2, 5
        }
        if (sceneNumber == 4)
        {
            blueSpawn = new Point(1, 4);
            redSpawn = new Point(19, 4);//18,2 -2, 5
        }
        GameObject tmp = Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        BluePortal = tmp.GetComponent<Spawn>();
        BluePortal.name = "BluePortal";
        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
    public bool InBounds(Point position)
    {
        return position.x >= 0 && position.y >= 0 && position.x < mapSize.x && position.y < mapSize.y;
    }
    public void GeneratePath()
    {
        path = Astar.GetFixedPath();
    }
    //public static bool IsNodeInFixedPath(TileScript tile)
    //{
    //    foreach(TileScript fixedTile in fixedPath)
    //    {
    //        if(tile == fixedTile)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    public void ResetLevelManager()
    {
        // Reiniciar las variables necesarias del LevelManager
        Tiles = null;
        path = null;
        BluePortal = null;
        
        // Restablecer otras variables según sea necesario

        // Detener y eliminar las torres existentes
        Tower[] towers = FindObjectsOfType<Tower>();
        foreach (Tower tower in towers)
        {
            Destroy(tower.gameObject);
        }
    }
}
