using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationComponent : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private MovementComponent movementComponent;
    public float debugValue;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("walking_speed", movementComponent.movementDirection.magnitude);  
        debugValue=movementComponent.movementDirection.magnitude;
    }
}
