using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MushroomAI : MonoBehaviour
{
    public float PathUpdateTime;
    private float pathTimer;

    public float StepUpdateTime;
    private float stepTimer;

    private GameObject player;

    private Tilemap floorTiles;
    private Tilemap wallsTiles;

    private List<Vector3Int> path;

    private int stepCounter;

    void Start()
    {
        floorTiles = GameObject.Find("floor").GetComponent<Tilemap>();
        wallsTiles = GameObject.Find("walls").GetComponent<Tilemap>();

        pathTimer = 0f;
        PathUpdateTime *= Time.deltaTime;
        stepCounter = 1;

        StepUpdateTime *= Time.deltaTime;
        stepTimer = 0f;
    }

    void FixedUpdate()
    {
        pathTimer += Time.deltaTime;

        if (path == null || pathTimer >= PathUpdateTime || stepCounter == path.Count)
        {
            var player = GameObject.Find("Player");
            var playerCoords = floorTiles.WorldToCell(player.transform.position);
            var mobCoords = floorTiles.WorldToCell(transform.position);

            path = FindPath(mobCoords, playerCoords);
            stepCounter = 0;
            pathTimer = 0f;
        }

        else
        {
            if (stepTimer >= StepUpdateTime)
            {
                transform.position = path[stepCounter] + new Vector3(0.5f, 0.5f);
                stepCounter++;
                stepTimer = 0f;
            }
            stepTimer += Time.deltaTime;
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
            var surTiles = SurroundTiles(tile);
            foreach (var nextTile in surTiles)
            {
                if (track.ContainsKey(nextTile)) continue;

                var wallTile = wallsTiles.GetTile((Vector3Int)nextTile);

                if (wallTile != null && wallTile.name == "wall4")
                    continue;

                track[nextTile] = tile;
                queue.Enqueue(nextTile);
            }
        }

        var pathItem = track[track[end]];
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
                if (Mathf.Abs(x) == Mathf.Abs(y)) continue;
                var vec = centralTile + new Vector3Int(x, y, 0);
                list.Add(vec);
            }
        return list;
    }

    public void ResetPath()
    {
        pathTimer = PathUpdateTime;
    }
}