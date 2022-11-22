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
    void Awake()
    {
        Physics.autoSyncTransforms = true;
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
    public void RestartGame(InputAction.CallbackContext context)
    {
        GameObjectDependencyManager.instance.ResetGameObjectDictionary();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = 0;
        health = 0;
        gameOver = false;
        UpdateUI();
    }
}
