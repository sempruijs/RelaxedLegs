using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _shootPoint;
    private Rigidbody2D _rb2d;
    public float bulletSpeed;
    public string transformFromTag;
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _shootPoint = GameObject.FindWithTag(transformFromTag).transform;
    }

    private void FixedUpdate()
    {
        _rb2d.AddForce(_shootPoint.up * bulletSpeed);    
    }
}
