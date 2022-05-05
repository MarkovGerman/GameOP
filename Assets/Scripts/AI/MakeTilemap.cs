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

    private Grid grid;
    private Tilemap tilesMap;
    private TilemapRenderer tilesRenderer;
    private TilemapCollider2D tilesCollider;
    private List<int[]> map;
    
    private void Awake()
    {
        map = ParseMapFile("1");

        gameObject.transform.position = new Vector3(0, 0, -75);
        grid = new GameObject("Grid").AddComponent<Grid>(); 
        
        grid.transform.SetParent(gameObject.transform);

        SetTilesMap();
    }

    void Start()
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

                tilesMap.SetTile(pos, tile);
            }
    }


    void Update()
    {
        
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

    private void SetTilesMap()
    {
        Tiles[0].colliderType  = Tile.ColliderType.None;
        Tiles[1].colliderType = Tile.ColliderType.Sprite;

        tilesMap = new GameObject("Tilesmap").AddComponent<Tilemap>();
        tilesRenderer = tilesMap.gameObject.AddComponent<TilemapRenderer>();
        tilesCollider = tilesMap.gameObject.AddComponent<TilemapCollider2D>();

        tilesMap.size = new Vector3Int(10, 10, 0);

        tilesMap.transform.SetParent(grid.transform);
    }

    private void ParseLine(string line, List<int[]> mapInt)
    {
        var line2Int = new int[line.Length];
        for (var i = 0; i < line.Length; i++)
        {
            var sym = line[i];
            switch (sym)
            {   
                case '.':
                    line2Int[i] = 0;
                    break;
                case 'w':
                    line2Int[i] = 1;
                    break;
                case 'p':
                    line2Int[i] = 2;
                    break;
                case 'm':
                    line2Int[i] = 3;
                    break;
                case ' ':
                    line2Int[i] = 4;
                    break;
            }
        }
        mapInt.Add(line2Int);
    }
}
