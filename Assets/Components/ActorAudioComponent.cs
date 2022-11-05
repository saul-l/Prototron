using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorAudioComponent : MonoBehaviour
{


    [SerializeField] private string shootAudioEvent;
    [SerializeField] private string damageAudioEvent;



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
