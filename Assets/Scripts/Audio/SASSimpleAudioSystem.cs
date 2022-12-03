/* Simple Audio System derived from SimpleAudioWrapper */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SASSimpleAudioSystem
{
    
    public static void PlayAudioEvent(string audioEvent, GameObject gObject)
    {
        gObject.GetComponent<AudioSource>().PlayOneShot(SASSoundBank.instance.GetAudioClipByName(audioEvent));
    }
}
