using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> musicList;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomMusic();
    }

    void PlayRandomMusic()
    {

        int randomIndex = Random.Range(0, musicList.Count);
        AudioClip randomMusic = musicList[randomIndex];

        audioSource.clip = randomMusic;
        audioSource.Play();

        Invoke("PlayRandomMusic", randomMusic.length);
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

}
