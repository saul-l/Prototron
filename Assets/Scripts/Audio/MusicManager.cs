using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private GameManager gameManager;
    private AudioSource audioSource;
    void Start()
    {
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        gameManager.gameStartEvent += StartMusic;
        gameManager.gameOverEvent += StopMusic;
    }

    // Update is called once per frame
    void StartMusic()
    {
        audioSource.Play();
    }

    void StopMusic()
    {
        audioSource.Stop();
    }
}
