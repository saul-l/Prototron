using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetComponent : MonoBehaviour
{

    [SerializeField] private Transform cameraArea;
    Transform cameraTarget;
    Vector3 targetPos;
    void Start()
    {
        cameraTarget = GameObjectDependencyManager.instance.GetGameObject("Player").GetComponent<Transform>();
    }
    void Update()
    {
        targetPos = cameraTarget.position;
        targetPos.x = Mathf.Max(cameraArea.transform.position.x-cameraArea.localScale.x*.5f, Mathf.Min(cameraArea.transform.position.x+cameraArea.localScale.x*.5f, targetPos.x));
        targetPos.z = Mathf.Max(cameraArea.transform.position.z-cameraArea.localScale.z*.5f, Mathf.Min(cameraArea.transform.position.z+cameraArea.localScale.z*.5f, targetPos.z));
        transform.position=targetPos;

      
    }
}
