using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed = 10f;
    public int health = 100;
    public int value = 50;
    public GameObject deathEffect;

    private Transform target;
    private int wavePointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Health before " + health);
        Debug.Log("Amount " + amount);
        health -= amount;
        Debug.Log("Health after " + health);
        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5);
        PlayerStats.Money += value;
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);  
    }
}
