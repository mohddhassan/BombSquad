using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float speed = 5.0f;
    
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && gameObject != null)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce (lookDirection * speed);
        }
        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }


    public void Destroyed()
    {
        Destroy(gameObject);
    }
}
