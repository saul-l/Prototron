using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
       
    public MovementComponent movementComponent;
    public GameObject player;
    Vector3 ThrowDirection;
    void Start()
    {
        movementComponent = this.GetComponent<MovementComponent>();
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        movementComponent.movementDirection = (player.transform.position-this.transform.position).normalized;
    }
}
