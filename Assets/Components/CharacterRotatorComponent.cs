using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotatorComponent : MonoBehaviour
{


    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private ShootingComponent shootingComponent;
    [SerializeField] private float rotationSpeed = 1.0f;
    private Quaternion lookAtTarget;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(shootingComponent != null && shootingComponent.shootingDirection != Vector3.zero)
        {
            lookAtTarget=Quaternion.LookRotation(shootingComponent.shootingDirection);
            
        }
        else if(movementComponent != null && movementComponent.movementDirection != Vector3.zero)
        {
            lookAtTarget=Quaternion.LookRotation(movementComponent.movementDirection);
        }
            
        transform.rotation=Quaternion.RotateTowards(transform.rotation, lookAtTarget, rotationSpeed*Time.deltaTime);
    }
}
