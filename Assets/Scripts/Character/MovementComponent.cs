/* MovementComponent handless character moving
 * 
 * Aims to provide both arcade style tight controls and ability to react to physics forces
 *
 * movementDirection determines direction.
 * movementSpeed determines speed.
 * movementChangeSpeed determines how quickly the change in direction happens. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementComponent : MonoBehaviour, IMovement
{
    public Vector3 movementDirection;


    Vector3 targetVelocity;
    Vector3 velocityChange;
    public float movementSpeed;
    public float movementChangeSpeed;
    public Rigidbody rb;

    void Awake()
    {
        rb=this.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (movementDirection!=Vector3.zero)
        {            
            targetVelocity=movementDirection*movementSpeed;

            // If we are already moving towards target direction with higher velocity, we can just keep that velocity.
            // Attempt to move towards direction should never slow us down.            
            if ((rb.velocity.x>0 && targetVelocity.x>0 && targetVelocity.x<rb.velocity.x) || (rb.velocity.x<0 && targetVelocity.x<0 && targetVelocity.x>rb.velocity.x))
            {
                targetVelocity.x=rb.velocity.x;
            }
            if ((rb.velocity.z>0 && targetVelocity.z>0 && targetVelocity.z<rb.velocity.z) || (rb.velocity.z<0 && targetVelocity.z<0 && targetVelocity.z>rb.velocity.z))
            {
                targetVelocity.z=rb.velocity.z;
            }
            
            velocityChange = targetVelocity-rb.velocity;

            velocityChange.x=Mathf.Clamp(velocityChange.x,-movementChangeSpeed,movementChangeSpeed);
            velocityChange.z=Mathf.Clamp(velocityChange.z,-movementChangeSpeed,movementChangeSpeed);
            velocityChange.y=0.0f;

            rb.AddForce(velocityChange,ForceMode.VelocityChange);
        }
    }

    Vector3 IMovement.movementDirection
    {
        get => movementDirection;
        set => movementDirection = value;
    }

}
