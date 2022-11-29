using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    private PlayerInput playerInput;
    private GameManager gameManager;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();

        playerInput = GetComponent<PlayerInput>();
        Debug.Log(" " + playerInput.playerIndex.ToString());

    }

    // Update is called once per frame


    void OnEnable()
    {
        // inputhandler.Player.Enable(); 

        playerInput.actions["Move"].performed += playerController.SetMovingDirection;
        playerInput.actions["Move"].canceled += playerController.SetMovingDirection;

        playerInput.actions["FireDown"].started += playerController.FireDown;
        playerInput.actions["FireDown"].canceled += playerController.FireDown;

        playerInput.actions["FireUp"].started += playerController.FireUp;
        playerInput.actions["FireUp"].canceled += playerController.FireUp;

        playerInput.actions["FireLeft"].started += playerController.FireLeft;
        playerInput.actions["FireLeft"].canceled += playerController.FireLeft;

        playerInput.actions["FireRight"].started += playerController.FireRight;
        playerInput.actions["FireRight"].canceled += playerController.FireRight;

    }

    void OnDisable()
    {
      //  inputhandler.Player.Disable();
    }
}
