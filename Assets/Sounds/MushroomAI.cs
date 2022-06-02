using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MushroomAI : MonoBehaviour
{
<<<<<<< HEAD
    public float Speed;

=======
    public bool IsStepping;
    public float Speed;
    public AudioSource Stepping;
>>>>>>> 765ac91493ecfb89f3c006b5abadece97a5daf30
    public GameObject DetectArea;
    public float PathUpdateTime;
    public float StepUpdateTime;

    private float pathTimer;
    private float stepTimer;

    private Tilemap floorTiles;
    private Tilemap wallsTiles;

    private List<Vector3Int> path;
    private int stepCounter;
    private Vector3 curTarget;

<<<<<<< HEAD
    private bool TurnSprite;
    private bool stepping = false;
    private Rigidbody2D rb;
    private Vector3 curTarget;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
=======
    private GameObject player;
    private Transform playerPos;

    void Start()
    {
        IsStepping = true;

        player = GameObject.FindGameObjectWithTag("Player");

>>>>>>> 765ac91493ecfb89f3c006b5abadece97a5daf30

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
        if (DetectArea.GetComponent<AreaDetector>().PlayerInArea && IsStepping)
        {
            pathTimer += Time.deltaTime;

<<<<<<< HEAD
            if (stepping)
            {
                if (Vector2.Distance(transform.position, curTarget) < 0.1f)
                {
                    rb.velocity = Vector2.zero;
                    stepping = false;
                }
=======
            playerPos = player.transform;

            if ((path == null || pathTimer >= PathUpdateTime || stepCounter == path.Count))
            {
                var playerCoords = floorTiles.WorldToCell(playerPos.position);
                var mobCoords = floorTiles.WorldToCell(transform.position);

                path = FindPath(mobCoords, playerCoords);
                stepCounter = 0;
                pathTimer = 0f;
>>>>>>> 765ac91493ecfb89f3c006b5abadece97a5daf30
            }

            else
            {
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
<<<<<<< HEAD
                    if (stepTimer >= StepUpdateTime)
                    {
                        stepping = true;
                        curTarget = path[stepCounter] + new Vector3(0.5f, 0.5f);

                        rb.velocity = (curTarget - transform.position).normalized * Speed;
=======
                    curTarget = path[stepCounter] + new Vector3(0.5f, 0.5f);

                    transform.position = Vector2.MoveTowards(transform.position, curTarget, 1f);

                    if (!Stepping.isPlaying) Stepping.Play();
>>>>>>> 765ac91493ecfb89f3c006b5abadece97a5daf30

                        stepCounter++;
                        stepTimer = 0f;
                    }
                    stepTimer += Time.deltaTime;
                }
<<<<<<< HEAD
=======
                stepTimer += Time.deltaTime;
                if (Vector2.Distance(playerPos.position, transform.position) < 1.5f)
                    Stepping.Stop();
>>>>>>> 765ac91493ecfb89f3c006b5abadece97a5daf30
            }
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

                if (wallTile != null && wallTile.name.Substring(0, 4) == "wall")
                    continue;

                track[nextTile] = tile;
                queue.Enqueue(nextTile);
            }
        }

        var pathItem = track[end];
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