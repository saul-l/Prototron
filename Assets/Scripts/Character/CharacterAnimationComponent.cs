using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationComponent : MonoBehaviour
{

    private Animator animator;
    private IMovement movementComponent;
    private IShooting shootingComponent;
    public float debugValue;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movementComponent = GetComponent<IMovement>();
        shootingComponent = GetComponent<IShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("walking_speed", movementComponent.movementDirection.magnitude);  
        debugValue=movementComponent.movementDirection.magnitude;

       if(shootingComponent!=null)
       { 
            if (shootingComponent.shootingDirection != Vector3.zero)
            { 
                animator.SetBool("aim", true);
            }
            else
            {
                animator.SetBool("aim", false);
            }
       }

    }

    public void Shoot()
    { 
        animator.SetTrigger("shoot");
    }
}
