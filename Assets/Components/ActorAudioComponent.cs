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
        AkSoundEngine.PostEvent(shootAudioEvent, gameObject);
    }

    public void deathAudio()
    {
        AkSoundEngine.PostEvent(deathAudioEvent, gameObject);
    }

    public void damageAudio()
    {
        AkSoundEngine.PostEvent(damageAudioEvent, gameObject);
    }

}
