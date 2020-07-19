using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterxSeconds : MonoBehaviour
{
    public float seconds;
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}