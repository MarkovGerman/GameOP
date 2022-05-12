using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterAI : MonoBehaviour
{
    private float PathUpdateTime = 40;
    private float pathTimer;

    private GameObject player;

    private Tilemap tilesMap;
    private List<Vector3Int> path;
    private HashSet<string> unsypportedTiles;
    private HashSet<Vector3> doorCoordinates;

    private int stepCounter;

    void Start()
    {
        doorCoordinates = GameObject.Find("map").GetComponent<MakeTilemap>().doorCoords;
        
        unsypportedTiles = new HashSet<string>();

        unsypportedTiles.Add("originWall");
        unsypportedTiles.Add("box");

        pathTimer = 0f;
        PathUpdateTime *= Time.deltaTime;
        stepCounter = 1;
    }

    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player(Clone)");
        if (tilesMap == null)
            tilesMap = GameObject.Find("baseLayer").GetComponent<Tilemap>();

        if (path == null || stepCounter == path.Count - 1 || pathTimer >= PathUpdateTime / 2)
        {
            path = FindPath((Vector3Int?)tilesMap.WorldToCell(gameObject.transform.position), (Vector3Int?)tilesMap.WorldToCell(player.transform.position));
            stepCounter = 1;
        }
          
        if (pathTimer >= PathUpdateTime && path != null && stepCounter < path.Count - 1)
        {
            var firstStep = path[stepCounter - 1];
            var secondStep = path[stepCounter];
            //Debug.Log("Moved");

            transform.Translate(secondStep - firstStep);

            pathTimer = 0f;
            stepCounter++;
        }
        pathTimer += Time.deltaTime;
    }

    private List<Vector3Int> FindPath(Vector3Int? start, Vector3Int? end)
    {
        var track = new Dictionary<Vector3Int?, Vector3Int?>();
        track[start] = null;

        var queue = new Queue<Vector3Int?>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {  
            var tile = queue.Dequeue();
            var incidentTiles = SurroundTiles(tile);
            var randomizer = Random.Range(0, incidentTiles.Count - 1);
            foreach (var nextTile in incidentTiles)
            {
                if (nextTile == incidentTiles[randomizer]) continue;
                if (track.ContainsKey(nextTile)) continue;

                var tile2Local = tilesMap.WorldToCell((Vector3Int)tile);

                if (unsypportedTiles.Contains(tilesMap.GetTile(tile2Local).name))
                    continue;
                if (doorCoordinates.Contains(tile2Local + new Vector3(0.5f, 0.5f, 0)))
                    continue;

                track[nextTile] = tile;
                queue.Enqueue(nextTile);
            }
            if (track.ContainsKey(end)) break;
        }
        var pathItem = end;
        var result = new List<Vector3Int>();
        while (pathItem != null)
        {
            result.Add((Vector3Int)pathItem);

            if (!track.ContainsKey(pathItem)) return null;
            pathItem = track[pathItem];
        }
        result.Reverse();

        return result;
    }

    private List<Vector3Int?> SurroundTiles(Vector3Int? centralTile)
    {
        var list = new List<Vector3Int?>();
        for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                var vec = centralTile + new Vector3Int(x, y, 0);
                list.Add(vec);
            }
        return list;
    }
}
