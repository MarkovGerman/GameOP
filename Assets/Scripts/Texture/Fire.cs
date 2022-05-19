using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	public float explosionRadius = 5;// радиус поражения
	public float power = 500;// сила взрыва	
	
	private Rigidbody[] physicObject;// тут будут все физ. объекты которые есть на сцене
	
	void Start(){
		physicObject = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];// Записываем все физ. объекты
		for(int i = 0; i < physicObject.Length; i++){
			if(Vector3.Distance(transform.position,physicObject[i].transform.position) <= explosionRadius){// Исключаем от обработки объекты которые достаточно далеко от взвыва
				physicObject[i].AddExplosionForce(power,transform.position,explosionRadius);// Создание взрыва, с силой power, в позиции transform.position, c радиусом explosionRadius
			}
		}
	}
}