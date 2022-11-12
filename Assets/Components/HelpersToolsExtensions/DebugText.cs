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

   

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
        GameManager.instance.UpdateUI();
    }

    public void PrintText(string textToPrint)
    {
        debugText.text = textToPrint;
    }

}
