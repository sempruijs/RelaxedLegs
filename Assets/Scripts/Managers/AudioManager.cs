using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    private AudioSource _audioSource;
    // public AudioClip inGameMusic;
    // public AudioClip menuMusic;
    
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
        _audioSource.Stop();
        _audioSource.Play();
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
