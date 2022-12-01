using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SAWSoundContainer", menuName = "SimpleAudioWrapper/SingleSoundContainer", order = 1)]
public class SimpleAudioWrapperSingleSoundContainer : SimpleAudioWrapperSoundContainer
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float volumeScale = 1.0f;

    public override void GetAudioClip(out AudioClip audioClip, out float volumeScale)
    {
        audioClip = this.audioClip;
        volumeScale = this.volumeScale;
    }

}