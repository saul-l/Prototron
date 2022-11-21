using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioWrapperRandomPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    AudioClip giveAudioClip()
    {
        return audioClips[Random.Range(0,audioClips.Length)];
    }
   
}
