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
        SimpleAudioWrapper.PlayAudioEvent(shootAudioEvent, gameObject);
    }

    public void deathAudio()
    {
        SimpleAudioWrapper.PlayAudioEvent(deathAudioEvent, gameObject);
    }

    public void meleeAudio()
    {
        SimpleAudioWrapper.PlayAudioEvent(deathAudioEvent, gameObject);
    }
    public void damageAudio()
    {
        SimpleAudioWrapper.PlayAudioEvent(damageAudioEvent, gameObject);
    }

}
