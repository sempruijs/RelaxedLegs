using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollideWithTag : MonoBehaviour
{
    public string theTag;
    
    //Audio
    public AudioClip withSound;

    public bool onTrigger = false;
    private AudioSource _audioSource;
    private bool _hasAudioClip;

    private void Start()
    {
        _audioSource = GameObject.FindWithTag("AudioManager").GetComponent<AudioSource>();
        _hasAudioClip = withSound != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!onTrigger)
        {
            if (other.gameObject.CompareTag(theTag))
            {
                HandelDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onTrigger)
        {
            if (other.gameObject.CompareTag(theTag))
            {
                HandelDestroy();
            }
        }
    }

    private void HandelDestroy()
    {
        if (_hasAudioClip)
        {
            _audioSource.PlayOneShot(withSound);
        }
        Destroy(gameObject);
    }
}
