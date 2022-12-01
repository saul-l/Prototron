// Simple texture uv scroller. Will be replaced with full shader implementation at some point, which just supplies time to shader.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextureScroller : MonoBehaviour
{
    private Material material;
    private Vector2 offSet;
    [SerializeField] Vector2 scrollDirection;
 
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offSet+=scrollDirection*Time.deltaTime;
        material.mainTextureOffset = offSet;
    }
}
