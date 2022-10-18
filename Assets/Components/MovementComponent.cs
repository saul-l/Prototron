using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementComponent : MonoBehaviour
{
    // these define character actions
    public Vector3 movementDirection;


    Vector3 targetVelocity;
    Vector3 velocityChange;
    public float movementSpeed;
    public float movementChangeSpeed;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        if (movementDirection!=Vector3.zero)
        {
            
            targetVelocity=movementDirection*movementSpeed;
            
            
            if ((rb.velocity.x>0 && targetVelocity.x>0 && targetVelocity.x<rb.velocity.x) || (rb.velocity.x<0 && targetVelocity.x<0 && targetVelocity.x>rb.velocity.x))
                targetVelocity.x=rb.velocity.x;
            if ((rb.velocity.z>0 && targetVelocity.z>0 && targetVelocity.z<rb.velocity.z) || (rb.velocity.z<0 && targetVelocity.z<0 && targetVelocity.z>rb.velocity.z))
                targetVelocity.z=rb.velocity.z;
            
            velocityChange = targetVelocity-rb.velocity;

            velocityChange.x=Mathf.Clamp(velocityChange.x,-movementChangeSpeed,movementChangeSpeed);
            velocityChange.z=Mathf.Clamp(velocityChange.z,-movementChangeSpeed,movementChangeSpeed);
            velocityChange.y=0.0f;

            rb.AddForce(velocityChange,ForceMode.VelocityChange);
        }


    }
}
