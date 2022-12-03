/* Fake soundbank for SimpleAudioWrapper
 * 
 * Usage:
 * -Place on instance to the scene.
 * -Add AudioClips to AudioClips array. Names should match Wwise events. */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SASSoundBank : MonoBehaviour
{
    public static SASSoundBank instance;
    [SerializeField] private SASSoundContainer[] soundContainers;
    private Dictionary<string, SASSoundContainer> soundContainerDictionary = new Dictionary<string, SASSoundContainer>();
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        for(int i = 0; i < soundContainers.Length; i++)
        {
            soundContainerDictionary.Add(soundContainers[i].name, soundContainers[i]);
        }

    }

    public AudioClip GetAudioClipByName(string audioClipName)
    {
        if (soundContainerDictionary.TryGetValue(audioClipName, out SASSoundContainer tmpSoundContainer))
        {
            AudioClip tmpAudioClip;
            float tmpVolume;
            tmpSoundContainer.GetAudioClip(out tmpAudioClip, out tmpVolume);
            return tmpAudioClip;
        }
        else
            return null;
    }




}
