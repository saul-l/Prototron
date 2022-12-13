using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraTargetComponent : MonoBehaviour
{
    [SerializeField] private Transform cameraAreaCenter;

    [SerializeField] private float cameraAreaX;    
    private Transform cameraTarget;
    private Vector3 targetPos;
    private Boolean active;
    private Boolean ultrawide;
    private GameManager gameManager;
    float currentAspectRatio;
    void Start()
    {
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();

        currentAspectRatio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().aspect;
        if (currentAspectRatio < (21.0f/9.0f))
        {
            ultrawide = false;
            // Yes, magic values.
            cameraAreaX = (1 - (currentAspectRatio / (21f / 9f))) * 40.0f;
        }
        else
        {
            ultrawide = true;
        }       
    }
    void Update()
    {
        if(active)
        {                       
            targetPos = cameraTarget.position;
            targetPos.x = Mathf.Max(cameraAreaCenter.transform.position.x-cameraAreaX, Mathf.Min(cameraAreaCenter.transform.position.x+ cameraAreaX, targetPos.x));
            targetPos.z = cameraAreaCenter.transform.position.z;
            transform.position=targetPos;
        }
        if (!active && !ultrawide && gameManager.amountOfAlivePlayers > 0)
        {         
            cameraTarget = GameObjectDependencyManager.instance.GetGameObject("Player").GetComponent<Transform>();
            active = true;
        }
        
    }
}
