using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    private MyGrid<PathNode> grid;
    public int x { get; private set; }
    public int y { get; private set; }

    public int GCost;
    public int HCost;
    public int FCost;

    public bool IsWalkable;
    public PathNode CameFrom;

    public PathNode(MyGrid<PathNode> grid , int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        IsWalkable = true;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }

    public override string ToString()
    {
        return x + ", " + y;
    }
}
