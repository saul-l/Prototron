using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTargetComponent : MonoBehaviour
{

    [SerializeField] private Transform cameraArea;
    private Transform cameraTarget;
    private Vector3 targetPos;
    private Boolean active;
    private Boolean ultrawide;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();

        if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().aspect < (21.0f/9.0f))
        {
            ultrawide = false;
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
            targetPos.x = Mathf.Max(cameraArea.transform.position.x-cameraArea.localScale.x*.5f, Mathf.Min(cameraArea.transform.position.x+cameraArea.localScale.x*.5f, targetPos.x));
            targetPos.z = Mathf.Max(cameraArea.transform.position.z-cameraArea.localScale.z*.5f, Mathf.Min(cameraArea.transform.position.z+cameraArea.localScale.z*.5f, targetPos.z));
            transform.position=targetPos;
        }
        if (!active && !ultrawide && gameManager.amountOfAlivePlayers > 0)
        {         
            cameraTarget = GameObjectDependencyManager.instance.GetGameObject("Player").GetComponent<Transform>();
            active = true;
        }
        
    }
}
