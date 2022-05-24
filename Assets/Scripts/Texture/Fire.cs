using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	public float explosionRadius =30;// радиус поражения
	public float power = 30;// сила взрыва	
	private Animator animator;
	
	private Rigidbody2D[] physicObjects;// тут будут все физ. объекты которые есть на сцене

	void Update()
	{
		physicObjects = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];// Записываем все физ. объекты
		animator = gameObject.GetComponent<Animator>();

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
		foreach (var physicObject in physicObjects)
		{
			// Исключаем от обработки объекты которые достаточно далеко от взвыва
				var position = transform.position;
				var position2D = new Vector2(position.x, position.y);
				var pos = new Vector2(position.x, position.y);
    			var pushDirection = (position2D - pos).normalized;

    			physicObject.AddForce(pushDirection * power, ForceMode2D.Impulse);
			
		}
        animator.SetBool("IsDead", true);
		Destroy(gameObject, 0.5f);
	}
	
}