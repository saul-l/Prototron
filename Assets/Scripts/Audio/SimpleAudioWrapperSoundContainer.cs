using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleAudioWrapperSoundContainer : ScriptableObject
{
    public virtual void GetAudioClip(out AudioClip audioClip, out float volumeScale)
    {
        audioClip = null;
        volumeScale = 1.0f;

    }

}

