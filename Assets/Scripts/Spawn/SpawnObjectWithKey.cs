using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectWithKey : MonoBehaviour
{
    public GameObject @object;
    public string withKey;
    
    //audio
    public AudioClip withSound;
    private AudioSource _audioSource;
    private bool _useSound;
    
    private bool _keyDown = false;

    private void Start()
    {
        _useSound = withSound != null;
        _audioSource = GameObject.FindWithTag("AudioManager").GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(withKey))
        {
            if (!_keyDown)
            {
                SpawnTheObject();
            }
            _keyDown = true;
        }
        else
        {
            _keyDown = false;
        }
    }

    private void SpawnTheObject()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Instantiate(@object, position, rotation);
        if (_useSound)
        {
            _audioSource.PlayOneShot(withSound);
        }
    }
}
