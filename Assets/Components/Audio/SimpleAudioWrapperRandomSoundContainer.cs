using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SAWSoundContainer", menuName = "SimpleAudioWrapper/RandomSoundContainer", order = 1)]
public class SimpleAudioWrapperRandomSoundContainer : SimpleAudioWrapperSoundContainer
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float volumeScale = 1.0f;

    public override void GetAudioClip(out AudioClip audioClip, out float volumeScale)
    {
        audioClip = audioClips[Random.Range(0, audioClips.Length)];
        volumeScale = this.volumeScale;
    }

  
}
