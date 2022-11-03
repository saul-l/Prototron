/* Simple Audio Wrapper between Wwise and Unity Audio
 * 
 * Currently only supports playing AudioClip instead of Wwise event.
 * Fake random containers coming at some point.
 * 
 * Usage:
 * - Add SimpleAudioWrapperSoundBank script to a game object in scene
 * - Add AudioClips to SimpleAudioWrapperSound AudioClips array. Names must match Wwise events!
 * - When you wish to play audio using wrapper use PlayAudioEvent function. It will automatically
 *   play either Wwise event or audio clip based on active platform.
 *   Like this: SimpleAudioWrapper.PlayAudioEvent("Play_test_event", gameObject);
 * - Game objects must have audiosources!
 * - Default config assumes only WebGL uses Unity native audio.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimpleAudioWrapper
{
    
    // This will one day be a wrapper/facade so that I can easily switch between Wwise and Unity native audio

    public static void PlayAudioEvent(string audioEvent, GameObject gObject)
    {
#if UNITY_WEBGL

        gObject.GetComponent<AudioSource>().PlayOneShot(SimpleAudioWrapperSoundBank.instance.GetAudioClipByName(audioEvent));
#else
        AkSoundEngine.PostEvent(audioEvent, gObject);
#endif
    }






}
