using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGravityComponent : MonoBehaviour
{
    [SerializeField] private float Extragravity = 5.0f;

    private Rigidbody rb;
    [SerializeField] private Vector3 height;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        height = new Vector3(0.0f, GetComponent<Collider>().bounds.size.y, 0.0f);
    }

    void FixedUpdate()
    {
        height = new Vector3(0.0f, GetComponent<Collider>().bounds.size.y, 0.0f);
        //Physics.Linecast(rb.velocity, rb.angularVelocity);
    }
}
