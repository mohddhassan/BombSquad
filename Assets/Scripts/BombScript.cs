using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float explosionDelay = 3f;
    private float countDown;
    private bool hasExploded = false;
    public float blastRadius = 5f;
    public float blastForce = 700f;
    void Start()
    {
        countDown = explosionDelay;

    }

    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
        
    }

    private void Explode()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius);

            }
            Enemy dest = nearbyObject.GetComponent<Enemy>();
            if (dest != null)
            {
                dest.Destroyed();
            }
        }

        Destroy(gameObject);
    }
}
