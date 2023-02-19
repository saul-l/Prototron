using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates character towards shooting direction and if shooting direction is zero, rotates towards movement direction
// movementComponent and/or shootingComponent should be configured in inspector
public class CharacterRotatorComponent : MonoBehaviour
{


    private IMovement movementComponent;
    private IShooting shootingComponent;
    [SerializeField] private float rotationSpeed = 10.8f;
    [SerializeField] private float rotationSpeedShoot = 39.6f;
    [SerializeField] private float shootingDirectionResetTime = 0.5f;
    private float rotationSpeedShootApplied;
    private Quaternion lookAtTarget;
    private Vector3 lookAtDirection = Vector3.zero;
    private float lastShotTime;
    private int frame;
    private void Start()
    {
        movementComponent = GetComponent<IMovement>();
        shootingComponent = GetComponent<IShooting>();
    }
    // Update is called once per frame
    void Update()
    {
        if (shootingComponent != null && shootingComponent.shootingDirection != Vector3.zero)
        {
            lookAtDirection = shootingComponent.shootingDirection;
            lookAtDirection.y = 0.0f;
            lookAtTarget = Quaternion.LookRotation(lookAtDirection, Vector3.up);
            lastShotTime = Time.time;
            rotationSpeedShootApplied = rotationSpeedShoot;
        }
        else
        {
            if (lastShotTime + shootingDirectionResetTime >= Time.time)
            {
                
                rotationSpeedShootApplied = 0.0f;
            }

            if (movementComponent != null && movementComponent.movementDirection != Vector3.zero)
            {
                rotationSpeedShootApplied = 0.0f;
                lookAtDirection = movementComponent.movementDirection;
                lookAtDirection.y = 0.0f;
                lookAtTarget = Quaternion.LookRotation(lookAtDirection, Vector3.up);
            }
        } 
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtTarget, Mathf.Max(rotationSpeed, rotationSpeedShootApplied));
    }
}
