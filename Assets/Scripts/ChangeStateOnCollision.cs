using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateOnCollision : MonoBehaviour
{
    //You give this script to a obstacle. NOT THE PLAYER!
    public string tag;
    public GameManager.State state;
    public bool isTrigger = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isTrigger)
        {
            if (other.gameObject.CompareTag(tag))
            {
                GameManager.Instance.state = state;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTrigger)
        {
            if (other.gameObject.CompareTag(tag))
            {
                GameManager.Instance.state = state;
            }
        }        
    }
}
