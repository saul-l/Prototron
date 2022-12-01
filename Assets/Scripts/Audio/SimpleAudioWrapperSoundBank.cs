/* Fake soundbank for SimpleAudioWrapper
 * 
 * Usage:
 * -Place on instance to the scene.
 * -Add AudioClips to AudioClips array. Names should match Wwise events. */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioWrapperSoundBank : MonoBehaviour
{
    public static SimpleAudioWrapperSoundBank instance;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private SimpleAudioWrapperSoundContainer[] soundContainers;
    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, SimpleAudioWrapperSoundContainer> soundContainerDictionary = new Dictionary<string, SimpleAudioWrapperSoundContainer>();
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        //Populate audioclip dictionary for quicker search.
        for (int i = 0; i < audioClips.Length; i++)
        {
            audioClipDictionary.Add(audioClips[i].name, audioClips[i]);

        }

        for(int i = 0; i < soundContainers.Length; i++)
        {
            soundContainerDictionary.Add(soundContainers[i].name, soundContainers[i]);
        }

    }

    public AudioClip GetAudioClipByName(string audioClipName)
    {
        if (soundContainerDictionary.TryGetValue(audioClipName, out SimpleAudioWrapperSoundContainer tmpSoundContainer))
        {
            AudioClip tmpAudioClip;
            float tmpVolume;
            tmpSoundContainer.GetAudioClip(out tmpAudioClip, out tmpVolume);
            return tmpAudioClip;
        }
       /*
        if (audioClipDictionary.TryGetValue(audioClipName, out AudioClip tmpAudioClip))
        {
            return audioClipDictionary[audioClipName];
        }*/
        else
            return null;
    }




}
