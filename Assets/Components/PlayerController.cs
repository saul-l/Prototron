using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    public MovementComponent movementComponent;
    public ShootingComponent shootingComponent;
    private Vector3 movementDirection;
    private Vector3 shootingDirection;
    public ButtonControl pressedButton;
    Directions.Direction aimingDirection;
    [SerializeField] UnityEvent FireEvent;

    public UniqueStack<Directions.Direction> aimFireBuffer = new UniqueStack<Directions.Direction>();
    void Start()
    {


        movementComponent = this.GetComponent<MovementComponent>();
        shootingComponent = this.GetComponent<ShootingComponent>();
    }
	


    public void SetMovingDirection(InputAction.CallbackContext context)
    {
        movementDirection=context.ReadValue<Vector2>();
        movementDirection.z=movementDirection.y;
        movementDirection.y=0;
        movementComponent.movementDirection=this.movementDirection;
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
      if(context.started) aimFireBuffer.AddNode(Directions.Direction.Left);
      if (context.canceled) aimFireBuffer.RemoveNode(Directions.Direction.Left);

      CalculateShootingDirectionAndFire();
    }

    public void FireRight(InputAction.CallbackContext context)
    {
        if (context.started) aimFireBuffer.AddNode(Directions.Direction.Right);
        if (context.canceled) aimFireBuffer.RemoveNode(Directions.Direction.Right);

        CalculateShootingDirectionAndFire();
    }

    public void FireUp(InputAction.CallbackContext context)
    {
        shootingComponent.shootingDirection = Vector3.up;
        if (context.started) aimFireBuffer.AddNode(Directions.Direction.Up);
        if (context.canceled) aimFireBuffer.RemoveNode(Directions.Direction.Up);

        CalculateShootingDirectionAndFire();
    }

    public void FireDown(InputAction.CallbackContext context)
    {
        if (context.started) aimFireBuffer.AddNode(Directions.Direction.Down);
        if (context.canceled) aimFireBuffer.RemoveNode(Directions.Direction.Down);

        CalculateShootingDirectionAndFire();
    }

    public void CalculateShootingDirectionAndFire()
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

public class Directions
{
    public enum Direction
    {
        None, Up, Down, Left, Right
    }

}
