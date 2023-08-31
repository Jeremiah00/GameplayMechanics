using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject focalpoint;
    Rigidbody rb;
    float fowardInput;
    public float speed;
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
    }
}
