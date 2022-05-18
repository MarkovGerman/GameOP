using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MakeTilemap : MonoBehaviour
{
    public Tile[] Tiles;
    public float TileSize;

    public GameObject Player;
    public GameObject Monster;
    public GameObject BossMonster;
    public GameObject Door;
    public GameObject Key;
    public GameObject Tank;

    public GameObject Potion;
    public GameObject Fire;
    public GameObject ElArrow;
    public GameObject Sharpes;

    private Dictionary<char, int> tilesDictionary;
    private Grid grid;
    private Tilemap baseLayer;
    private List<int[]> map;

    public HashSet<Vector3> doorCoords;

    private void Awake()
    {
        doorCoords = new HashSet<Vector3>();
        tilesDictionary = new Dictionary<char, int>();
        PrepareDictionary(@"D:\GameOP\Assets\TilesDecode\forDict.txt");

        map = ParseMapFile("1");

        gameObject.transform.position = new Vector3(0, 0, -75);

        grid = new GameObject("Grid").AddComponent<Grid>(); 
        grid.transform.SetParent(gameObject.transform);

        SetTilesMap();
        DrawMap();  
    }

    private void PrepareDictionary(string filePath)
    {
        var streamReader = new StreamReader(filePath);
        var line = streamReader.ReadLine();

        while (line != null)
        {
            tilesDictionary[line[0]] = int.Parse(line.Substring(1, 2).ToString());
            line = streamReader.ReadLine();
        }

        streamReader.Close();
    }

    private List<int[]> ParseMapFile(string mapNum, string filePath = @"D:\GameOP\Assets\Maps\map")
    {
        var streamReader = new StreamReader(filePath + mapNum + ".txt");
        var line = streamReader.ReadLine();
        var mapInt = new List<int[]>();

        while (line != null)
        {
            ParseLine(line, mapInt);
            line = streamReader.ReadLine();
        }

        streamReader.Close();

        return mapInt;
    }

    private void ParseLine(string line, List<int[]> mapInt)
    {
        var line2Int = new int[line.Length];
        for (var i = 0; i < line.Length; i++)
        {
            var sym = line[i];
            line2Int[i] = tilesDictionary[sym];
        }
        mapInt.Add(line2Int);
    }

    private void SetTilesMap()
    {
        Tiles[0].colliderType  = Tile.ColliderType.None;
        Tiles[1].colliderType = Tile.ColliderType.Grid;

        baseLayer = new GameObject("baseLayer").AddComponent<Tilemap>();
        baseLayer.transform.SetParent(grid.transform);
        baseLayer.size = new Vector3Int(10, 10, 0);

        baseLayer.gameObject.AddComponent<TilemapRenderer>();
        baseLayer.gameObject.AddComponent<TilemapCollider2D>();

        baseLayer.gameObject.layer = 0;
    }

    private void DrawMap()
    {
        for (var i = 0; i < map.Count; i++)
            for (var j = 0; j < map[i].Length; j++)
            {
                var pos = new Vector3Int(j, -i, 0);
                var tile = Tiles[map[i][j]];

                if (map[i][j] == 2)
                {
                    Instantiate(Player, pos, transform.rotation);
                }

                if (map[i][j] == 3)
                {
                    Instantiate(Monster, pos, transform.rotation);
                }

                if (map[i][j] == 6)
                {
                    Instantiate(BossMonster, pos, transform.rotation);
                }

                if (map[i][j] == 5)
                {
                    Instantiate(Door, pos + new Vector3(0.5f, 0.5f, 0), transform.rotation);
                    doorCoords.Add(pos + new Vector3(0.5f, 0.5f, 0));
                }

                if (map[i][j] == 7)
                {
                    Instantiate(Key, pos, transform.rotation);
                }

                if (map[i][j] == 8)
                {
                    Instantiate(Tank, pos, transform.rotation);
                }

                if (map[i][j] == 9)
                {
                    Instantiate(Sharpes, pos, transform.rotation);
                }

                if (map[i][j] == 10)
                {
                    Instantiate(Fire, pos, transform.rotation);
                }

                if (map[i][j] == 11)
                {
                    Instantiate(Potion, pos, transform.rotation);
                }

                if (map[i][j] == 12)
                {
                    Instantiate(ElArrow, pos, transform.rotation);
                }

                baseLayer.SetTile(pos, tile);
            }
    }
}
