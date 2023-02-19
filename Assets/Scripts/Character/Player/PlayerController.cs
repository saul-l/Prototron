/* Player Controller handles player controls
 * 
 * Invoke events with Player Input component
 * 
 * Requires MovementComponent and ShootingComponent in same prefab
 *
 * Restarts game through GameManager. It's ugly, but works for now.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    private IMovement movementComponent;
    private IShooting shootingComponent;
    private Vector3 movementDirection;
    private Vector3 shootingDirection;
    public ButtonControl pressedButton;
    Directions.Direction aimingDirection;
    [SerializeField] UnityEvent FireEvent;
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool fourWay = true;
    GameManager gameManager;
    public UniqueStack<Directions.Direction> aimFireBuffer = new UniqueStack<Directions.Direction>();
    void Start()
    {
        movementComponent = this.GetComponent<IMovement>();
        shootingComponent = this.GetComponent<IShooting>();
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
    }
	
    void IsDead()
    {
        isDead = true;
    }

    public void SetMovingDirection(InputAction.CallbackContext context)
    {
        if(!isDead)
        { 
            movementDirection=context.ReadValue<Vector2>();
            movementDirection.z=movementDirection.y;
            movementDirection.y=0;
            movementComponent.movementDirection=this.movementDirection;
        }
    }

    public void SetAimingDirection(InputAction.CallbackContext context)
    {
        this.shootingDirection=context.ReadValue<Vector2>();
        shootingDirection.z=shootingDirection.y;
        shootingDirection.y=0;
        shootingComponent.shootingDirection=this.shootingDirection;

        // Analog controller shooting direction logic, which used to be in ShootingComponent
        // const float fourer = 2.0f / Mathf.PI;
        // const float antiFourer = 1.0f / fourer;
        //  angle = fourer * Mathf.Atan2(shootingDirection.x, shootingDirection.z);
        // angle = Mathf.Round(angle);
        //  angle *= antiFourer;
        //  newShootingDirection.z = Mathf.Cos(angle);
        //  newShootingDirection.x = Mathf.Sin(angle);
    }



    public void FireLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aimFireBuffer.AddNode(Directions.Direction.Left);
            shootingDirection.x -= 1.0f;
        }
        if (context.canceled)
        {
            aimFireBuffer.RemoveNode(Directions.Direction.Left);
            shootingDirection.x += 1.0f;
        }

      CalculateShootingDirectionAndFire();
    }

    public void FireRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aimFireBuffer.AddNode(Directions.Direction.Right);
            shootingDirection.x += 1.0f;
        }
        if (context.canceled)
        {
            aimFireBuffer.RemoveNode(Directions.Direction.Right);
            shootingDirection.x -= 1.0f;
        }

        CalculateShootingDirectionAndFire();
    }

    public void FireUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aimFireBuffer.AddNode(Directions.Direction.Up);
            shootingDirection.z += 1.0f;
        }
        if (context.canceled)
        {
            aimFireBuffer.RemoveNode(Directions.Direction.Up);
            shootingDirection.z -= 1.0f;
        }

        CalculateShootingDirectionAndFire();
    }

    public void FireDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aimFireBuffer.AddNode(Directions.Direction.Down);
            shootingDirection.z -= 1.0f;
        }
        if (context.canceled)
        {
            aimFireBuffer.RemoveNode(Directions.Direction.Down);
            shootingDirection.z += 1.0f;
        }

        CalculateShootingDirectionAndFire();
    }

    public void CalculateShootingDirectionAndFire()
    {
        if (!isDead)
        {
            if(fourWay)
            { 
                if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Up)
                {
                    shootingComponent.shootingDirection = Vector3.forward;
                }

                if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Down)
                {
                    shootingComponent.shootingDirection = Vector3.back;
                }

                if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Left)
                {
                    shootingComponent.shootingDirection = Vector3.left;
                }

                if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Right)
                {
                    shootingComponent.shootingDirection = Vector3.right;
                }
            }
            else
            {
                shootingComponent.shootingDirection = shootingDirection;
            }
            if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.None)
            {
                shootingComponent.shootingDirection = Vector3.zero;
                shootingComponent.fire = false;
            }
            else
            {
                shootingComponent.fire = true;
            }
        }
    }
}

public class Directions
{
    public enum Direction
    {
        None, Up, Down, Left, Right
    }

}
