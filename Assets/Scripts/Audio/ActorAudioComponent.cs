// Handles actor specific audio events.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorAudioComponent : MonoBehaviour
{
    [SerializeField] private string damageAudioEvent;
    [SerializeField] private string deathAudioEvent;

    public void deathAudio()
    {
        SASSimpleAudioSystem.PlayAudioEvent(deathAudioEvent, gameObject);
    }

    public void meleeAudio()
    {
        SASSimpleAudioSystem.PlayAudioEvent(deathAudioEvent, gameObject);
    }
    public void damageAudio()
    {
        SASSimpleAudioSystem.PlayAudioEvent(damageAudioEvent, gameObject);
    }

}
