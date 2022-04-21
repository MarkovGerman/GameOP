using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Node
{
    public Vector3? Position{get;set;}
    public float PathLengthFromStart{get;set;}
    public float BestLengthToGoal{get; set;}
    public float ApproximateDistance{
        get
        {
            return PathLengthFromStart + BestLengthToGoal;
        }
    }

    public Node Previous;
    
    public Node(Vector3 position, float pathLengthFromStart, float bestDistanceToGoal, Node previous)
    {
        Position = position;
        PathLengthFromStart = pathLengthFromStart;
        BestLengthToGoal = bestDistanceToGoal;
        Previous = previous;
    }

    public static Node GetNode(Vector3 position, Vector3 start,  Vector3 goal, Node previous)
    {
        return new Node(position, Vector3.Distance(position, start), Vector3.Distance(position, goal), previous);
    }
}
public class WalkingEnemy : MonoBehaviour
{
    private Rigidbody2D rigidBodyComponent;
    public GameObject player;
    private HashSet<Vector3> boxes = new HashSet<Vector3>();
    private float timeBetweenFrames;
    void Start()
    {
        timeBetweenFrames = Time.deltaTime * 8;
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        var boxes1 = GameObject.FindGameObjectsWithTag("Box");
        var hashSetBoxes = new HashSet<Vector3>();
        foreach (var box in boxes1)
            hashSetBoxes.Add(box.transform.position);
    }

   
    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindWithTag("Player");
        var coor = rigidBodyComponent.transform.position;
        var coordinatesPlayer = player.transform.position;
        timeBetweenFrames -= Time.deltaTime;
        var newPath = new List<Vector3>();
        if (Vector3.Distance(coor, coordinatesPlayer) <= 30 && timeBetweenFrames <= 0)
        {
            timeBetweenFrames = Time.deltaTime * 8;
            newPath = GetPathToTarget(coor, coordinatesPlayer);
        }
        for (var i=1; i< newPath.Count; i++)
        {
            var direction = newPath[i] - newPath[i - 1];
            transform.Translate(direction);
        }
    }
    
    public List<Vector3> GetPathToTarget(Vector3 start, Vector3 goal)
    {
		var pointsToOpen = new HashSet<Node>();
		var closedPoints = new HashSet<Vector3>();
        var startNode = Node.GetNode(start, start, goal, null);
		var track = new Dictionary<Vector3, Node> {[start] = startNode};
		pointsToOpen.Add(startNode);
        Node node = startNode;
        var finishFlag = true;
        while(finishFlag)
        {
            node = FindBestNode(pointsToOpen);
            Debug.Log($"{node.Position.Value.x}:{node.Position.Value.y}");
            foreach (var point in AddPoint(start, startNode, goal, start,  closedPoints))
                pointsToOpen.Add(point);
            if (node.Position != null)
            {
                closedPoints.Add(node.Position.Value);

                if (node.Position.Value == goal)
                    finishFlag = false;
                if (pointsToOpen.Count == 0) finishFlag = false;
            }

            if (pointsToOpen.Count >= 1000) finishFlag = false;
        }
        var result = new List<Vector3>();

        while (node != null)
        {
            result.Add(node.Position.Value);
            node = node.Previous;
        }
        result.Reverse();
        return result;
    }

    public IEnumerable<Node> AddPoint(Vector3 point, Node previous, Vector3 goal, Vector3 start,  HashSet<Vector3> closedPoints)
    {
        var vectors = new []
        {
            new Vector3(point.x + 1, point.y),
            new Vector3(point.x - 1, point.y),
            new Vector3(point.x, point.y+ 1),
            new Vector3(point.x, point.y - 1),
        };
        var list = new List<Node>();
        //Ошибка в этом методе при проверке на присутсвие стены
        foreach(var node in vectors.Select(vector => Node.GetNode(vector, start, goal, previous)))
        {
            if (!boxes.Contains(node.Position.Value) && !closedPoints.Contains(node.Position.Value))
                list.Add(node);
        }

        return list;
    }

    public Node FindBestNode (HashSet<Node> pointsToOpen)
    {
        return pointsToOpen.OrderBy(point => point.BestLengthToGoal).First();
    }

    
   
}
