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
    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();
    
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

    }

    public AudioClip GetAudioClipByName(string audioClipName)
    {
       
        if (audioClipDictionary.TryGetValue(audioClipName, out AudioClip tmpAudioClip))
        {
            return audioClipDictionary[audioClipName];
        }
        else
            return null;
    }




}
