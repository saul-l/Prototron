using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public InputHandler inputhandler;
    private GameManager gameManager;
    private PlayerController playerController1;
    private PlayerController playerController2;
    // Start is called before the first frame update
    void Awake()
    {
        playerController1 = GameObjectDependencyManager.instance.GetGameObject("Player").GetComponent<PlayerController>();
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();

        inputhandler = new InputHandler();
    }

    // Update is called once per frame


    void OnEnable()
    {
        inputhandler.Player.Enable(); 
        inputhandler.Player.Move.performed += playerController1.SetMovingDirection;
        inputhandler.Player.Move.canceled += playerController1.SetMovingDirection;
        
        inputhandler.Player.FireDown.started += playerController1.FireDown;
        inputhandler.Player.FireDown.canceled += playerController1.FireDown;
        
        inputhandler.Player.FireUp.started += playerController1.FireUp;
        inputhandler.Player.FireUp.canceled += playerController1.FireUp;
        
        inputhandler.Player.FireLeft.started += playerController1.FireLeft;
        inputhandler.Player.FireLeft.canceled += playerController1.FireLeft;

        inputhandler.Player.FireRight.started += playerController1.FireRight;
        inputhandler.Player.FireRight.canceled += playerController1.FireRight;

        inputhandler.Player.Restart.started += gameManager.RestartGame;       
    }

    void OnDisable()
    {
        inputhandler.Player.Disable();
    }
}
