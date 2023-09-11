using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    Transform target;
    float speed = 15.0f;
    bool homing;

    float rocketStrength = 15.0f;
    float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if(homing && target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position = moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRb.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);

    }
}
