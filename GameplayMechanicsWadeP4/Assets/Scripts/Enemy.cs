using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    Rigidbody enemyRb;
    GameObject player;
    public bool isBoss =false;
    public float spawnInterval; 
    private float nextSpawn; 
    public int miniEnemySpawnCount; 
    private SpawnManager spawnManager;
    private Vector3 r;
    void Start()
    {
        
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if(isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirectrion = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirectrion * speed);
        if(isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn= Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
        if (transform.position.y < -0.5f)
        {
            Destroy(gameObject);
        }
    }
    private void s()
    {
        
    }
}
