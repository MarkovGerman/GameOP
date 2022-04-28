using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    private const int moveStraightCost = 10;
    private const int moveDiagonalCost = 14;

    private MyGrid<PathNode> grid;
    private List<PathNode> toOpen;
    private HashSet<PathNode> opened;

    public PathFinding(int width, int height)
    {
        grid = new MyGrid<PathNode>(width, height, 1f, new Vector3(-8, -8), (MyGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public MyGrid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        var startNode = grid.GetValue(startX, startY);
        var endNode = grid.GetValue(endX, endY);

        if (startNode == null || endNode == null)
        {
            // Invalid Path
            return null;
        }

        toOpen = new List<PathNode> { startNode };
        opened = new HashSet<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                var pathNode = grid.GetValue(x, y);
                pathNode.GCost = 99999999;
                pathNode.CalculateFCost();
                pathNode.CameFrom = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (toOpen.Count > 0)
        {
            PathNode currentNode = GetLowesFCostNode(toOpen);
            if (currentNode == endNode)
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            toOpen.Remove(currentNode);
            opened.Add(currentNode);

            foreach (var neighbourNode in GetSurroundingsList(currentNode))
            {
                if (opened.Contains(neighbourNode)) continue;
                if (!neighbourNode.IsWalkable)
                {
                    opened.Add(neighbourNode);
                    continue;
                }

                var tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.GCost)
                {
                    neighbourNode.CameFrom = currentNode;
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!toOpen.Contains(neighbourNode))
                    {
                        toOpen.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private List<PathNode> GetSurroundingsList(PathNode currentNode)
    {
        var surroundings = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            surroundings.Add(GetNode(currentNode.x - 1, currentNode.y));
            if (currentNode.y - 1 >= 0) surroundings.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.GetHeight()) surroundings.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }

        if (currentNode.x + 1 < grid.GetWidth())
        {
            surroundings.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y - 1 >= 0) surroundings.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.GetHeight()) surroundings.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        if (currentNode.y - 1 >= 0) surroundings.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < grid.GetHeight()) surroundings.Add(GetNode(currentNode.x, currentNode.y + 1));

        return surroundings;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetValue(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        var path = new List<PathNode>();
        path.Add(endNode);
        var currentNode = endNode;

        while (currentNode.CameFrom != null)
        {
            path.Add(currentNode.CameFrom);
            currentNode = currentNode.CameFrom;
        }
        path.Reverse();

        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        var xDistance = Mathf.Abs(a.x - b.x);
        var yDistance = Mathf.Abs(a.y - b.y);
        var remaining = Mathf.Abs(xDistance - yDistance);

        return moveDiagonalCost * Mathf.Min(xDistance, yDistance) + moveStraightCost * remaining;
    }

    private void PreCalculateStart(PathNode startNode, PathNode endNode)
    {

        for (int x = 0; x < grid.GetWidth(); x++)
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                var pathNode = grid.GetValue(x, y);
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.CameFrom = null;
            }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();
    }

    private PathNode GetLowesFCostNode(List<PathNode> pathNodeList)
    {
        var lowesFCostNode = pathNodeList[0];

        for (var i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].FCost < lowesFCostNode.FCost)
                lowesFCostNode = pathNodeList[i];
        }
        return lowesFCostNode;
    }
}
