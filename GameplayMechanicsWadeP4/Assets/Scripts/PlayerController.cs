using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PowerUp;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject focalpoint;
    Rigidbody rb;
    float fowardInput;
    public float speed;
    public bool hasPowerup;
    float powerStrength = 120.0f;
    public GameObject powerupIndicator;

    public PowerUpType currentPowerUp = PowerUpType.None; 
    public GameObject rocketPrefab; 
    private GameObject tmpRocket; 
    private Coroutine powerupCountdown;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalpoint = GameObject.Find("Focal Point");
    }


    // Update is called once per frame
    void Update()
    {
        fowardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector3.forward * fowardInput * speed);
        rb.AddForce(Vector3.right * horizontalInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.PushBack)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector2 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Player collided with " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
            enemyRb.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
        }
    }
    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);

        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }
}
