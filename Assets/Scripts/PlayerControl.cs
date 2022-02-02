using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public GameObject player;
    public bool hasPowerUp = false;
    private float powerUpStrength = 10;
    public GameObject powerUpIndicator;
    private bool playerExtraLife = false;
    private bool isPlayerDestroyed = false;

    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        
    }

    
    void Update()
    {
        PlayerMovement();
        PowerUpIndication();
        DestroyPlayerBelowGround();
        //InstantiateIfLifePicked();        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(PowerUpCountdown());
            
        }

        if (other.CompareTag("Health"))
        {
            
            Destroy(other.gameObject);
            playerExtraLife = true;
            Debug.Log("Player picked extra life = " + playerExtraLife);
            

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidBody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdown()
    {     
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }


    private void PlayerMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * verticalInput, ForceMode.VelocityChange);
    } 

    private void PowerUpIndication()
    {
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.3f, 0);
    }

    private void DestroyPlayerBelowGround()
    {
        if (transform.position.y < -10)
        {
            
            Destroy(gameObject);
            isPlayerDestroyed = true;
            Debug.Log("Player is destroyed = " + isPlayerDestroyed);

        }
    }

    private void InstantiateIfLifePicked()
    {
        if (playerExtraLife && isPlayerDestroyed)
        {
            Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            playerExtraLife = false;
            Debug.Log("New Player Instantiated");
        }
    }
}
