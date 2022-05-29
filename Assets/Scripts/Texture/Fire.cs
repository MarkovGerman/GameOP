using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	public float explosionRadius = 30;// радиус поражения
	public float power = 30;// сила взрыва	
	private Animator animator;
	private Vector3 pos;
	private Vector2 position;
	private GameObject player;
	public float explodeForce;
	
	private Collider2D[] colliders;// тут будут все физ. объекты которые есть на сцене

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		player = GameObject.Find("Player");
	}

	void Update()
	{
	}

	void OnTriggerEnter2D(Collision2D collision)
    {
		GetComponent<Collider2D>().enabled = false;
		pos = transform.position;
		colliders = Physics2D.OverlapCircleAll(pos, 10);

        if (collision.gameObject.CompareTag("Bullet"))
        {
			Boom();
			Debug.Log("Попал");
			Destroy(collision.gameObject);
		}
    }

	public void Boom()
	{
		animator.SetBool("IsDead", true);
		if ((transform.position - player.transform.position).magnitude <= 5)
			player.GetComponent<Health>().SelfHealth -= 1;
		// foreach (var collider in colliders)
		// {
		// 	var other = collider.attachedRigidbody; 
   		// 	var direction = (other.position - position).normalized;
   		// 	var distance = (other.position - position).magnitude;
   		// 	var force = direction * explodeForce * other.mass / distance;
   		// 	other.AddForceAtPosition(position, force, ForceMode2D.Impulse);
		// other.velocity += force;
		// }

		Destroy(gameObject, 0.5f);
	}
	
}