using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAudioComponent : MonoBehaviour
{


    [SerializeField] string shootAudioEvent;
    // Start is called before the first frame update

    public void shootAudio()
    {
        AkSoundEngine.PostEvent(shootAudioEvent, gameObject);
    }

}
