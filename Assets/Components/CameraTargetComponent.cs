using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CameraTargetComponent : MonoBehaviour
{

    [SerializeField] private Transform cameraArea;
    private Transform cameraTarget;
    private Vector3 targetPos;
    private Boolean active;

    void Start()
    {        
        if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().aspect < (21.0f/9.0f))
        {
            active = true;
        }
        else
        {
            active = false;
        }

        if(active)
        { 
            cameraTarget = GameObjectDependencyManager.instance.GetGameObject("Player").GetComponent<Transform>();
        }
        Debug.Log("Aspect ratio:" + GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().aspect + " 21:9 " + 21.0f / 9.0f);
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
    }
}
