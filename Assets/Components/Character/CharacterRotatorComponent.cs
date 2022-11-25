using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates character towards shooting direction and if shooting direction is zero, rotates towards movement direction
// movementComponent and/or shootingComponent should be configured in inspector
public class CharacterRotatorComponent : MonoBehaviour
{


    private IMovement movementComponent;
    private IShooting shootingComponent;
    [SerializeField] private float rotationSpeed = 1.0f;
    private Quaternion lookAtTarget;
    private Vector3 lookAtDirection = Vector3.zero;

    private void Start()
    {
        movementComponent = GetComponent<IMovement>();
        shootingComponent = GetComponent<IShooting>();
        rotationSpeed = rotationSpeed * 100.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(shootingComponent != null && shootingComponent.shootingDirection != Vector3.zero)
        {
            lookAtDirection = shootingComponent.shootingDirection;
            lookAtDirection.y = 0.0f;
            lookAtTarget = Quaternion.LookRotation(lookAtDirection);            
        }
        else if(movementComponent != null && movementComponent.movementDirection != Vector3.zero)
        {
            lookAtDirection = movementComponent.movementDirection;
            lookAtDirection.y = 0.0f;
            lookAtTarget =Quaternion.LookRotation(lookAtDirection);
        }
            
        transform.rotation=Quaternion.RotateTowards(transform.rotation, lookAtTarget, rotationSpeed*Time.deltaTime);
    }
}
