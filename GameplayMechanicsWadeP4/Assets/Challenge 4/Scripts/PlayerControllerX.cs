using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;
    public ParticleSystem smokeParticle;
    float boostSpeed = 1000;
    private bool canBoost = true;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;
    private Coroutine powerupCountdown;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        smokeParticle = GameObject.Find("Smoke_Particle").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        bool spacebarPressed = Input.GetKeyDown(KeyCode.Space);
        if (spacebarPressed && canBoost)
        {
            playerRb.AddForce(focalPoint.transform.forward * boostSpeed * Time.deltaTime, ForceMode.Impulse);
            StartCoroutine("SpeedBoostCooldown");
        }
        // this means that we are currently boosting, so make smoke particle follow player
        else if (canBoost == false)
        {
            float playerPosX = this.transform.position.x;
            float playerPosZ = this.transform.position.z;
            // make y position -1 so that it's at the bottom of the player
            smokeParticle.transform.position = new Vector3(playerPosX, -1, playerPosZ);

            // Set powerup indicator position to beneath player
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        }

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    IEnumerator SpeedBoostCooldown()
    {
        speed = 1000;
        smokeParticle.Play();
        yield return new WaitForSeconds(5);
        smokeParticle.Stop();
        speed = 500;   // back to the original speed
        canBoost = false;
        yield return new WaitForSeconds(5);
        canBoost = true;
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.transform.position - other.gameObject.transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
