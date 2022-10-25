using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
       
    public MovementComponent movementComponent;
    public MeleeComponent meleeComponent;
    public GameObject player;
    Vector3 ThrowDirection;
    private Transform myTransform;
    private Transform playerTransform;
    void Awake()
    {
        movementComponent = this.GetComponent<MovementComponent>();
        player = GameObject.Find("Player");      
        playerTransform = player.transform;
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        movementComponent.movementDirection = (playerTransform.position-myTransform.position).normalized;
        
        //temp melee attack. proper one will use child collider an some weapon system
        if(Vector3.Distance(playerTransform.position, myTransform.position) < 1.5f)
        {
            DebugText.instance.PrintText("lol");
            player.GetComponent<IPlayerDamageable>().ApplyDamage(2);
        }

    }
}
