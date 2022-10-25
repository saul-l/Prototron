#define WWISE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSystem
{
    // This will one day be a wrapper/facade so that I can easily switch between Wwise and Unity native audio

#if WWISE
    public static void PlayAudioEvent(string AudioEvent)
    {

    }

#endif

}
