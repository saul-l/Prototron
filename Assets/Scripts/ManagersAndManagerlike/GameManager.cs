using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;  
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public int health = 0;
    public bool gameOver = false;
    public int amountOfAlivePlayers;
    public bool gameStarted;
    public bool keyboardPlayer;
    private Vector3 SpawnPosition;
    [SerializeField] GameObject player;
    private List<InputDevice> activeGamepads = new List<InputDevice>();
    void Awake()
    {
        Application.targetFrameRate = 60;
        Physics.autoSyncTransforms = true;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            RestartGame();
        }

        if (keyboardPlayer==false && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AddPlayer(false);
        }

        if (Gamepad.current != null)
        {
            if(Gamepad.current.startButton.wasPressedThisFrame)
            {
                RestartGame();
            }

            if(!activeGamepads.Contains(Gamepad.current.device))
            { 
                if(Gamepad.current.buttonEast.wasPressedThisFrame ||
                   Gamepad.current.buttonWest.wasPressedThisFrame ||
                   Gamepad.current.buttonNorth.wasPressedThisFrame ||
                   Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    AddPlayer(true);
                }
            }
        }

        if(gameOver)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) RestartGame();

            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonEast.wasPressedThisFrame ||
                    Gamepad.current.buttonWest.wasPressedThisFrame ||
                    Gamepad.current.buttonNorth.wasPressedThisFrame ||
                    Gamepad.current.buttonSouth.wasPressedThisFrame)
                {
                    RestartGame();
                }
            }
        }
            
        if (amountOfAlivePlayers<1 && gameStarted)
        {
            gameOver = true;
            UpdateUI();
        }
    }

    private void AddPlayer(bool gamepad)
    {
        GameObject newPlayer = Instantiate(player, SpawnPosition, Quaternion.identity);

        if(gamepad)
        {
            newPlayer.GetComponent<PlayerInput>().SwitchCurrentControlScheme("GamePad", Gamepad.current.device);
            activeGamepads.Add(Gamepad.current.device);            
        }
        else
        { 
            newPlayer.GetComponent<PlayerInput>().SwitchCurrentControlScheme("KeyboardMouse", Keyboard.current.device);
            keyboardPlayer = true;
        }

        newPlayer.GetComponent<IDamageable>().EventDead += PlayerDied;
        
        amountOfAlivePlayers++;
        if (!gameStarted)
        {
            gameStarted = true;
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        if (gameOver)
        {
            DebugText.instance.PrintText("Score: " + score + " Press space or gamepad button to restart");
        }
        else if (!gameStarted)
        {
            DebugText.instance.PrintText("Press space or gamepad button to start");
        }
        else if (gameStarted && !gameOver)
        {
            DebugText.instance.PrintText("Score: " + score + " Health: " + health);
        }


    }

    public void PlayerDied()
    {
        amountOfAlivePlayers--;
    }
    public void RestartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
      //  SceneManager.UnloadSceneAsync(currentScene);
        GameObjectDependencyManager.instance.ResetGameObjectDictionary();        
        SceneManager.LoadScene(currentScene);
        score = 0;
        health = 0;
        gameOver = false;
        gameStarted = true;
        UpdateUI();
    }
}
