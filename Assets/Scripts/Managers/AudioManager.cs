using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    private AudioSource _audioSource;
    public AudioSource inGameMusicAudioSource;
    
    // public AudioClip inGameMusic;
    public AudioClip menuMusic;
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
        Stop();
        inGameMusicAudioSource.Play();
    }

    public void MenuMusic()
    {
        Stop();
        PlayAudioClip(menuMusic);
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
        PlayAudioClip(pickUpCoinSounds[Random.Range(0, pickUpCoinSounds.Length)]);
    }
}
