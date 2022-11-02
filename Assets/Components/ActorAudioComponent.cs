using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorAudioComponent : MonoBehaviour
{

    [SerializeField] private string shootAudioEvent;
    [SerializeField] private string damageAudioEvent;
    [SerializeField] private string deathAudioEvent;

    public void shootAudio()
    {
#if !UNITY_WEBGL
        AkSoundEngine.PostEvent(shootAudioEvent, gameObject);
#endif
    }

    public void deathAudio()
    {
#if !UNITY_WEBGL
        AkSoundEngine.PostEvent(deathAudioEvent, gameObject);
#endif
    }

    public void damageAudio()
    {
#if !UNITY_WEBGL
        AkSoundEngine.PostEvent(damageAudioEvent, gameObject);
#endif    
    }

}
