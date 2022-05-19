using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxin : MonoBehaviour
{
    float explosionRadius = 5;// радиус поражения
	float power = 50;// сила взрыва	
	Rigidbody[] physicObject;// тут будут все физ. объекты которые есть на сцене
    void Start()
    {
    	physicObject = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];// Записываем все физ. объекты
    }
    
    void Update(){}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Boom();
            Destroy(gameObject);
            //PlayAnimation();            
        }
    }

    void Boom()
    {
        for(int i = 0; i < physicObject.Length; i++){
			if(Vector3.Distance(transform.position, physicObject[i].transform.position) <= explosionRadius){// Исключаем от обработки объекты которые достаточно далеко от взвыва
				physicObject[i].AddExplosionForce(power, transform.position, explosionRadius);// Создание взрыва, с силой power, в позиции transform.position, c радиусом explosionRadius
			}
		}
    }

    //void PlayAnimation(){}
}
