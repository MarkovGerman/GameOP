using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozeAttack : MonoBehaviour
{
    private GameObject player;
    private float timeInFroze;
    private bool IsFrozen = false;
    public float timeFrozen;
    public float Count;

    void Start()
    {
        player = GameObject.Find("Player");
        timeFrozen *= Time.deltaTime;
        timeInFroze = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFrozen && timeInFroze < timeFrozen)
            timeInFroze += Time.deltaTime;
        else if (timeInFroze >= timeFrozen)
        {
            IsFrozen = false;
            player.GetComponent<Player>().AntiFroze();
            timeInFroze = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Froze(Count); //замедляем игрока в 2 раза
            IsFrozen = true;
        }
    }
}
