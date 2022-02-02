using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject bombPrefab;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ThrowBomb();
        }
        
    }

    private void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
       
    }
}
