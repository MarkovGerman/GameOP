// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class Node
// {
//     public Vector3? Position{get;set;}
//     public float PathLengthFromStart{get;set;}
//     public float BestLengthToGoal{get; set;}
//     public float ApproximateDistance{
//         get
//         {
//             return PathLengthFromStart + BestLengthToGoal;
//         }
//     }


//     public Node Previous;
    
//     public Node(Vector3 position, float pathLengthFromStart, float bestDistanceToGoal, Node previous)
//     {
//         Position = position;
//         PathLengthFromStart = pathLengthFromStart;
//         BestLengthToGoal = bestDistanceToGoal;
//         Previous = previous;
//     }

//     public static Node GetNode(Vector3 position, Vector3 start,  Vector3 goal, Node previous)
//     {
//         return new Node(position, Vector3.Distance(position, start), Vector3.Distance(position, goal), previous);
//     }
// }
// public class WalkingEnemy : MonoBehaviour
// {
//     private Rigidbody2D rigidBodyComponent;
//     public GameObject player;
//     public GameObject[] boxes;

//     void Start()
//     {
//         rigidBodyComponent = GetComponent<Rigidbody2D>();
//         player = GameObject.FindWithTag("Player");
//         boxes = GameObject.FindGameObjectsWithTag("Box");
//         var coorBoxes = new HashSet<Vector3>();
//         foreach (var box in boxes)
//             coorBoxes.Add(box.transform.rotation * Vector3.forward);
//     }

   
//     // Update is called once per frame
//     void Update()
//     {
//         player = GameObject.FindWithTag("Player");
//         var targetVector = transform.rotation * Vector3.forward - player.transform.rotation* Vector3.forward;
//     }
    
//     public List<Node> GetPathToTarget(Vector3 start, Vector3 goal, HashSet<Vector3> boxes)
//     {
//         var maxLengthPath = 30;
// 		var pointsToOpen = new HashSet<Vector3>();
// 		var visitedPoints = new HashSet<Vector3>();
// 		var track = new Dictionary<Vector3, Node> {[start] = Node.GetNode(start, start, goal, null)};
// 		pointsToOpen.Add(start);

//         var finishFlag = true;
//         while(finishFlag)
//         {
            
//         }
//     }

//     public IEnumerable<Node> AddPoint(Vector3 point, Node previous, Vector3 goal, Vector3 start, HashSet<Vector3> boxes)
//     {
//         var vectors = new []
//         {
//             new Vector3(point.x + 1, point.y),
//             new Vector3(point.x - 1, point.y),
//             new Vector3(point.x, point.y+ 1),
//             new Vector3(point.x, point.y - 1),
//         };
        
//         return vectors.Select(vector => Node.GetNode(vector, start, goal, previous))
//                       .Where(node => !boxes.Contains(node.Position.Value));        
//     }   
// }

// public class DijkstraData
// 	{
// 		public Vector3? Previous { get; set; }
// 		public int Price { get; set; }
// 	}
	
// 	public class DijkstraPathFinder
// 	{
// 		public IEnumerable<PathWithCost> GetPathsByDijkstra(HashSet<Vector3> boxes, Vector3 start)
// 		{
// 			var pointsToOpen = new HashSet<Vector3>();
// 			var visitedPoints = new HashSet<Vector3>();
// 			var track = new Dictionary<Vector3, DijkstraData> {[start] = new DijkstraData { Price = 0, Previous = null }};
// 			pointsToOpen.Add(start);

// 			while (true)
// 			{
// 				Vector3? toOpen = null;
// 				var bestPrice = int.MaxValue;
// 				foreach (var pointToOpen in pointsToOpen
// 					.Where(pointToOpen => track.ContainsKey(pointToOpen) && track[pointToOpen].Price < bestPrice))
// 				{
// 					bestPrice = track[pointToOpen].Price;
// 					toOpen = pointToOpen;
// 				}
// 				if (toOpen == null) yield break;
// 				if (hashSetTargets.Contains(toOpen.Value)) yield return FindPath(track, toOpen.Value);
// 				foreach (var incidentEdge in GetPoints(toOpen.Value, state))
// 					SelectPoint(track, incidentEdge, state, pointsToOpen, visitedPoints, toOpen.Value);
// 				pointsToOpen.Remove(toOpen.Value);
// 				visitedPoints.Add(toOpen.Value);
// 			}
// 		}

// 		private void SelectPoint(Dictionary<Vector3, DijkstraData> track, Vector3 point,
// 			State state, HashSet<Vector3> pointsToOpen, HashSet<Vector3> visitedPoints, Point toOpen)
// 		{
// 			var currentPrice = state.CellCost[point.X, point.Y] + track[toOpen].Price;
// 			if (!track.ContainsKey(point) || track[point].Price > currentPrice)
// 				track[point] = new DijkstraData { Previous = toOpen, Price = currentPrice };
// 			if (!visitedPoints.Contains(point))
// 				pointsToOpen.Add(point);
// 		}
		
// 		private PathWithCost FindPath(Dictionary<Vector3, DijkstraData> track, Vector3 end)
// 		{
// 			var result = new List<Vector3>();
// 			Vector3? currentPoint = end;
// 			while (!(currentPoint is null))
// 			{
// 				result.Add(currentPoint.Value);
// 				currentPoint = track[currentPoint.Value].Previous;
// 			}
// 			result.Reverse();
// 			return new PathWithCost(track[end].Price, result.ToArray());
// 		}

// 		private IEnumerable<Vector3> GetPoints(Vector3 node, State state)
// 		{
// 			return new[]
// 			{
// 				new Vector3(node.x, node.y + 1, node.z),
// 				new Vector3(node.x, node.y - 1, node.z),
// 				new Vector3(node.x + 1, node.y, node.z),
// 				new Vector3(node.x - 1, node.y, node.z)
// 			}.Where(point => state.InsideMap(point) && !state.IsWallAt(point));
// 		}
// 	}
