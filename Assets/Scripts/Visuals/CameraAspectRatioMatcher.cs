using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraAspectRatioMatcher : MonoBehaviour
{
    private RenderBuffer colorBuffer;
    private RenderBuffer mainCameraColorBuffer;
    private RenderBuffer depthBuffer;
    public RenderTexture renderTexture;
    public Camera mainCamera;
    private Camera myCamera;

    private void Start()
    {
        
        myCamera = GetComponent<Camera>();
        mainCamera.SetTargetBuffers(mainCameraColorBuffer, depthBuffer);
        myCamera.SetTargetBuffers(colorBuffer, depthBuffer);
        myCamera.targetTexture = renderTexture;
    }

    void Update()
    {
        myCamera.aspect=mainCamera.aspect;
        
    }
}
