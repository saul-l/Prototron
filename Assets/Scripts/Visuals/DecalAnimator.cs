using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DecalAnimator : MonoBehaviour
{
    [SerializeField] private DecalProjector decalProjector;
    [SerializeField] float fadeFactor;
    [SerializeField] Vector3 size;
    void Start()
    {
        decalProjector = GetComponent<DecalProjector>();
    }

    // Update is called once per frame
    void Update()
    {
        decalProjector.fadeFactor = fadeFactor;
        decalProjector.size = size;
    }
}
