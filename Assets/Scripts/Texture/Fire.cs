using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	public float explosionRadius = 30;// радиус поражения
	public float power = 30;// сила взрыва	
	private Animator animator;
	private Vector3 pos;
	private Vector2 position;
	private GameObject player;
	public float explodeForce = 3f;

	[SerializeField] private AudioSource explosionSound;

	public float DamageRadius;
	
	private Collider2D[] colliders;// тут будут все физ. объекты которые есть на сцене

	void Start()
	{
		explodeForce *= 1000f;
		animator = gameObject.GetComponent<Animator>();
		player = GameObject.Find("Player");
	}

	void Update()
	{
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Bullet"))
		{		
			Boom();
			Debug.Log("Попал");
		}
	}

	public void Boom()
	{
		animator.SetBool("IsDead", true);
		//if ((pos - player.transform.position).magnitude <= 5)
		//	player.GetComponent<Health>().SelfHealth -= 1;

		explosionSound.Play();
		colliders = Physics2D.OverlapCircleAll(transform.position, DamageRadius);
		foreach (var collider in colliders)
		{
			if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Mob")
			{
				collider.gameObject.GetComponent<Health>().SelfHealth -= 1; // Damage

				var gameObj = collider.gameObject;
				var rb = gameObj.GetComponent<Rigidbody2D>();


				// var other = collider.attachedRigidbody;
				var distVec = (gameObj.transform.position - pos);
				var distance = distVec.magnitude;
				var direction = distVec.normalized;

				
				var force = direction * explodeForce / (rb.mass * distance); // вычисляем силу взрыва тела в зависимости от расстояния и массы
																			 //other.AddForceAtPosition(position, force, ForceMode2D.Impulse);
																			 //other.velocity += force;
				if (collider.gameObject.tag == "Player") 
					gameObj.GetComponent<Player>().OffController();
				rb.velocity += (Vector2)force;

			}
		}
		Destroy(gameObject, 0.5f);

	}

    private void OnDrawGizmos()
    {
		Gizmos.DrawWireSphere(transform.position, DamageRadius);
    }

}