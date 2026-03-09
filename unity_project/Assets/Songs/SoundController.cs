using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip somDano; // Arraste o som aqui pelo Inspector
    public static AudioClip _somDano; // Arraste o som aqui pelo Inspector

    public AudioClip[] musicPlaylist;
    public AudioSource audioSource;
    
    private int currentIndex = -1;

    void Start()
    {
        _somDano = somDano;

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        PlayNextSong();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        if (musicPlaylist.Length == 0) return;

        // Escolhe uma música aleatória diferente da anterior
        int nextIndex;
        do {
            nextIndex = Random.Range(0, musicPlaylist.Length);
        } while (musicPlaylist.Length > 1 && nextIndex == currentIndex);

        currentIndex = nextIndex;
        audioSource.clip = musicPlaylist[currentIndex];
        audioSource.Play();
    }
}
    
