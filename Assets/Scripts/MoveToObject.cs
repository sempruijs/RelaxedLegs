using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    public float speed;
    public GameObject point;

    private bool _shouldBeMoving = true;
    
    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.InGame)
        {
            if (_shouldBeMoving)
            {
                float step =  speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, point.transform.position, step);
            }
        }
    }

    private void OnCollisionExit2D()
    {
        _shouldBeMoving = true;
    }

    private void OnCollisionEnter2D()
    {
        _shouldBeMoving = false;
    }
}
