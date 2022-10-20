using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    

    public UniqueStack<Directions.Direction> aimFireBuffer = new UniqueStack<Directions.Direction>();
    void Start()
    {
        //This should be in some general manager, but we don't have one yet, so it's here for now.
        Physics.autoSyncTransforms = true;

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
    }



    public void FireLeft(InputAction.CallbackContext context)
    {
      if(context.started)
            aimFireBuffer.AddNode(Directions.Direction.Left);
      if (context.canceled)
            aimFireBuffer.RemoveNode(Directions.Direction.Left);

      CalculateShootingDirectionFromEnumDirection();
    }

    public void FireRight(InputAction.CallbackContext context)
    {
        if (context.started)
            aimFireBuffer.AddNode(Directions.Direction.Right);
        if (context.canceled)
            aimFireBuffer.RemoveNode(Directions.Direction.Right);

        CalculateShootingDirectionFromEnumDirection();
    }

    public void FireUp(InputAction.CallbackContext context)
    {
        shootingComponent.shootingDirection = Vector3.up;
        if (context.started)
            aimFireBuffer.AddNode(Directions.Direction.Up);
        if (context.canceled)
            aimFireBuffer.RemoveNode(Directions.Direction.Up);

        CalculateShootingDirectionFromEnumDirection();
    }

    public void FireDown(InputAction.CallbackContext context)
    {
        if (context.started)
            aimFireBuffer.AddNode(Directions.Direction.Down);
        if (context.canceled)
            aimFireBuffer.RemoveNode(Directions.Direction.Down);

        CalculateShootingDirectionFromEnumDirection();
    }

    public void CalculateShootingDirectionFromEnumDirection()
    {

        if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Up)
            shootingComponent.shootingDirection = Vector3.forward;

        if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Down)
            shootingComponent.shootingDirection = Vector3.back;

        if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Left)
            shootingComponent.shootingDirection = Vector3.left;

        if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.Right)
            shootingComponent.shootingDirection = Vector3.right;

        if (aimFireBuffer.returnFirstNodeValue() == Directions.Direction.None)
            shootingComponent.shootingDirection = Vector3.zero;
    }

}

public class Directions
{
    public enum Direction
    {
        None, Up, Down, Left, Right
    }

}
