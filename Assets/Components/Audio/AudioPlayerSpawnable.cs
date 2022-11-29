using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerSpawnable : MonoBehaviour
{
    [SerializeField] private string AudioEvent;
    // Start is called before the first frame update
    void OnEnable()
    {
        SimpleAudioWrapper.PlayAudioEvent(AudioEvent, gameObject);
    }
}
