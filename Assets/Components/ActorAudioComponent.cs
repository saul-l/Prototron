using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorAudioComponent : MonoBehaviour
{

#if UNITY_WEBGL
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private string shootAudioEvent;
#else
    [SerializeField] private string shootAudioEvent;
    [SerializeField] private string damageAudioEvent;
    [SerializeField] private string deathAudioEvent;
#endif


    public void shootAudio()
    {
        SimpleAudioWrapper.PlayAudioEvent(shootAudioEvent, gameObject);


    }

    public void deathAudio()
    {

    }

    public void damageAudio()
    {
#if !UNITY_WEBGL
        AkSoundEngine.PostEvent(damageAudioEvent, gameObject);
#endif
    }

}
