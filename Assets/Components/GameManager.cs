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
        Physics.autoSyncTransforms = true;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            RestartGame();
        }

        if (keyboardPlayer==false && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject newPlayer = Instantiate(player, SpawnPosition, Quaternion.identity);
            newPlayer.GetComponent<PlayerInput>().SwitchCurrentControlScheme("KeyboardMouse", Keyboard.current.device);
            keyboardPlayer = true;
            amountOfAlivePlayers++;
            if (!gameStarted) gameStarted = true;
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
                    amountOfAlivePlayers++;
                    GameObject newPlayer = Instantiate(player, SpawnPosition, Quaternion.identity);
                    newPlayer.GetComponent<PlayerInput>().SwitchCurrentControlScheme("GamePad", Gamepad.current.device);
                    activeGamepads.Add(Gamepad.current.device);

                    if (!gameStarted) gameStarted = true;
                }
            }
        }

        if (amountOfAlivePlayers<1 && gameStarted)
        {
            gameOver = true;
        }
    }
    public void UpdateUI()
    {
        if (!gameOver)
        { 
            DebugText.instance.PrintText("Score: " + score + " Health: " + health);
        }
        else
        { 
            DebugText.instance.PrintText("Score: " + score + " Press Enter or ESC");
        }
    }
    public void RestartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(currentScene);
        GameObjectDependencyManager.instance.ResetGameObjectDictionary();        
        SceneManager.LoadScene(currentScene);
        score = 0;
        health = 0;
        gameOver = false;
        gameStarted = true;
        UpdateUI();
    }
}
