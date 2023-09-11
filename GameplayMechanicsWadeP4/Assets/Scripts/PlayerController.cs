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
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector2 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRb.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
