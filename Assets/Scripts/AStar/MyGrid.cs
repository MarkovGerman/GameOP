using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyGrid<TGridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public bool DebugMode = true;

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPos;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugCellArray;

    public MyGrid(int width, int height, float cellSize, Vector3 originPos, Func<MyGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new TGridObject[width, height];
        debugCellArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
        if (DebugMode) DrawCellMap();
    }

    private void DrawCellMap()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugCellArray[x, y] = NameCell(x, y);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
            debugCellArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
        };
    }

    private TextMesh NameCell(int x, int y)
    {
        var gameObject = new GameObject("World_Text", typeof(TextMesh));
        var transform = gameObject.transform;

        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.SetParent(null, false);
        transform.localPosition = GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f;

        var textMesh = gameObject.GetComponent<TextMesh>();

        textMesh.text = x + ", " + y;
        textMesh.fontSize = 30;
        textMesh.anchor = TextAnchor.MiddleCenter;

        return textMesh;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }

    public Vector2Int GetXY(Vector3 worldPos)
    {
        var x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        var y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);

        return new Vector2Int(x, y);
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugCellArray[x, y].text = gridArray[x, y].ToString();
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPos, TGridObject value)
    {
        var vec = GetXY(worldPos);
        SetValue(vec.x, vec.y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
            return gridArray[x, y];
        return default(TGridObject);
    }

    public TGridObject GetValue(Vector3 worldPos)
    {
        var vec = GetXY(worldPos);
        return GetValue(vec.x, vec.y);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
 