using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + tileSize * x, worldStart.y - tileSize * y, 0), map);
    }
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
    private void SpawnPortals()
    {
        blueSpawn = new Point(1, 2);
        GameObject tmp = Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        BluePortal = tmp.GetComponent<Spawn>();
        BluePortal.name = "BluePortal";

        redSpawn = new Point(5, 5);
        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
    public bool InBounds(Point position)
    {
        return position.x >= 0 && position.y >= 0 && position.x < mapSize.x && position.y < mapSize.y;
    }
    public void GeneratePath()
    {
        path = Astar.GetPath(blueSpawn, redSpawn);
    }
}
