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

    void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
			Boom();
			Debug.Log("Попал");
		}
    }

    public void Boom()
	{
		IsBoom = true;
		foreach (var physicObject in physicsObject)
		{
			// Исключаем от обработки объекты которые достаточно далеко от взвыва
				var position = transform.position;
				var position2D = new Vector2(position.x, position.y);
				var pos = new Vector2(position.x, position.y);
    			var pushDirection = (position2D - pos).normalized;

    			physicObject.AddForce(pushDirection * power, ForceMode2D.Impulse);
			
		}
        animator.SetBool("IsBoom", true);
		time = Time.deltaTime;
		Destroy(gameObject, 5f);
	}
}
