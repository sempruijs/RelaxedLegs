using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    private AudioSource _audioSource;
    public AudioSource inGameMusicAudioSource;

    public bool music = true;
    public bool sfx = true;
    
    public AudioClip menuMusicAudioClip;
    public AudioClip[] pickUpCoinSounds;
    
    private static AudioManager _instance;
    public static AudioManager Instance {
        get {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        } 
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void InGameMusic()
    {
        if (music)
        {
            Stop();
            inGameMusicAudioSource.Play();   
        }
    }

    public void MenuMusic()
    {
        if (music)
        {
            Stop();
            PlayAudioClip(menuMusicAudioClip);            
        }
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip, 1f);
    }

    public void Stop()
    {
        _audioSource.Stop();
        inGameMusicAudioSource.Stop();
    }

    public void PickUpCoin()
    {
        if (sfx)
        {
            PlayAudioClip(pickUpCoinSounds[Random.Range(0, pickUpCoinSounds.Length)]);    
        }
    }
}
