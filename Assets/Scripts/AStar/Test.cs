using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Hero;
    public float delta;
    private float currentDelta;
    private PathFinding pathFinding;

    private void Start()
    {
        delta = Time.deltaTime * 30;
        currentDelta = 0f;
        pathFinding = new PathFinding(16, 16);
    }

    private void Update()
    { 
        if (currentDelta >= delta)
        {
            var botPos = pathFinding.GetGrid().GetXY(gameObject.transform.position);
            var heroPos = Hero.transform.position;
            var vec = pathFinding.GetGrid().GetXY(heroPos);
            var path = pathFinding.FindPath(botPos.x, botPos.y, vec.x, vec.y);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y)  + new Vector3(-7.5f, -7.5f), new Vector3(path[i + 1].x, path[i + 1].y)  + new Vector3(-7.5f, -7.5f), Color.green, 5f);
                }
            }

            currentDelta = delta;
        }
        currentDelta += Time.deltaTime;
    }

}
