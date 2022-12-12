using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraAspectRatioMatcher : MonoBehaviour
{
    public Camera mainCamera;
    private Camera myCamera;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        myCamera.aspect=mainCamera.aspect;
    }
}
