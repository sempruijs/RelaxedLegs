using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectBetweenPoints : MonoBehaviour
{
    [Header("Points")]
    public GameObject pointA;
    public GameObject pointB;

    public float speed;

    public bool bounce = false;
    public string withTag;

    private bool tagShouldMove = false;

    private bool boolFromScript;
    
    public enum Activate
    {
        WhenGameStarts,
        WhenCollideWithTag,
        WhenTriggerWithTag,
        WhenAllObjectsAreDeletedWithTag,
        WhenAllObjectsAreDeletedFromList
    };

    public Activate activate = Activate.WhenGameStarts;


    private bool _toPointB = true;
    void Start()
    {
        transform.position = pointA.transform.position;
    }

    void Update()
    {
        if (activate == Activate.WhenGameStarts)
        {
            if (GameManager.Instance.state == GameManager.State.InGame)
            {
                Move();
            }
        }

        if (activate == Activate.WhenCollideWithTag && tagShouldMove)
        {
            Move();
        }
        
        if (activate == Activate.WhenTriggerWithTag && tagShouldMove)
        {
            Move();
        }


        if (activate == Activate.WhenAllObjectsAreDeletedFromList)
        {
            
        }
    }
    
    void MoveTo(GameObject point)
    {
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, point.transform.position, step);
    }

    private void Move()
    {
        if (bounce)
        {
            //This is responsible for deciding to wich point the object should go. 
            if (transform.position == pointB.transform.position)
            {
                _toPointB = false;
            } else if (transform.position == pointA.transform.position)
            {
                _toPointB = true;
            }

            if (_toPointB)
            {
                MoveTo(pointB);
            }
            else
            {
                MoveTo(pointA);
            }
        }
        else
        {
            MoveTo(pointB);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(withTag) && activate == Activate.WhenCollideWithTag)
        {
            tagShouldMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(withTag) && activate == Activate.WhenTriggerWithTag)
        {
            tagShouldMove = true;
        }
    }
}
