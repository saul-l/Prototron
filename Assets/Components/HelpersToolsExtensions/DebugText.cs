using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public static DebugText instance;


    public TextMeshProUGUI debugText;
    private GameManager gameManager;
   

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
        gameManager.UpdateUI();
    }

    public void PrintText(string textToPrint)
    {
        debugText.text = textToPrint;
    }

}
