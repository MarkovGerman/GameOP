using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterAI : MonoBehaviour
{
    private float PathUpdateTime = 30;

    private GameObject player;
    private Tilemap tilesMap;
    private float pathTimer;
    private List<Vector3Int> path;

    void Start()
    {
        pathTimer = 0f;
        PathUpdateTime *= Time.deltaTime;
    }

    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player(Clone)");
        if (tilesMap == null)
            tilesMap = GameObject.Find("Tilesmap").GetComponent<Tilemap>();


        path = FindPath((Vector3Int?)tilesMap.WorldToCell(gameObject.transform.position), (Vector3Int?)tilesMap.WorldToCell(player.transform.position));

        foreach (var step in path)
        {
            while (pathTimer < PathUpdateTime)
                pathTimer += Time.deltaTime;
            Debug.Log("Moved");
            transform.Translate((tilesMap.CellToWorld(step) - transform.position) * 0.001f);
            pathTimer = 0f;
        } 
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
            foreach (var nextTile in SurroundTiles(tile))
            {
                if (track.ContainsKey(nextTile)) continue;

                var tile2Local = tilesMap.WorldToCell((Vector3Int)tile);

                if (tilesMap.GetTile(tile2Local).name == "originWall")
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
            pathItem = track[pathItem];
        }
        result.Reverse();

        return result;
    }

    private IEnumerable<Vector3Int?> SurroundTiles(Vector3Int? centralTile)
    {
        for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                var vec = centralTile + new Vector3Int(x, y, 0);
                yield return vec;
            }
    }
}
