using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Rigidbody enemyRb;
    GameObject player;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirectrion = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirectrion * speed);
        if (transform.position.y < -0.5f)
        {
            Destroy(gameObject);
        }
    }
}
