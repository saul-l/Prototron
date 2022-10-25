using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public static DebugText debugTextInstance;


    public TextMeshProUGUI debugText;

    private string testString = "trololol";

    void Awake()
    {
        if (debugTextInstance != null && debugTextInstance != this)
            Destroy(this);
        else
            debugTextInstance = this;
    }

    void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
    }

    public void PrintText(string testToPrint)
    {
        debugText.text = testToPrint;
    }

}
