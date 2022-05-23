using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    private float health;
    private ScoreManagement sm;

    public int Damage = 1;
    public GameObject Key;
    public GameObject Heal;

    private void Start()
    { 
        sm = FindObjectOfType<ScoreManagement>();
    }

    private void Update()
    {
        health = GetComponent<Health>().SelfHealth;
        if (health <= 0)
        {
            sm.Add();
            Destroy(gameObject);
        }
    }
}
