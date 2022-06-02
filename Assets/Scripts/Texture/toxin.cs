using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxin : MonoBehaviour
{
    float explosionRadius = 5;// радиус поражения
	private bool IsBoom = false;
	float time;
	float power = 50;// сила взрыва	
	Rigidbody2D[] physicsObject;// тут будут все физ. объекты которые есть на сцене
    private Animator animator;
	private GameObject player;

    void Start()
    {
    	physicsObject = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];// Записываем все физ. объекты
        animator = gameObject.GetComponent<Animator>();
		player =  GameObject.Find("Player");
    }
    
    void Update()
	{
		if ((transform.position - player.transform.position).magnitude < 3 && IsBoom)
		{
			if (time >= Time.deltaTime * 120)
			{
				player.GetComponent<Health>().SelfHealth -= 1;
				time = Time.deltaTime;
			}
			else
				time += Time.deltaTime;
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Bullet"))
		{
			GetComponent<Collider2D>().enabled = false;
			Boom();
			Debug.Log("Попал");
		}
	}

    public void Boom()
	{
		IsBoom = true;
        animator.SetBool("IsBoom", true);
		time = Time.deltaTime;
		Destroy(gameObject, 5f);
	}
}
