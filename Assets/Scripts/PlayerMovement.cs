using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
      //Input
      private float _moveHorizontal;
      private float _moveVertical;

      private Vector2 _movement;
      
      //Speed
      public float moveSpeed;
      public float jumpForce;
      
      //Data
      public int amountOfJumps;
      private int _jumpsLeft;
      
      public bool turnAroundAnimation;
      public bool topDown;

      //Components
      private Rigidbody2D _rb2d;
      private AudioSource _audioSource;
      
      //Sound
      [Header("AudioClips")]
      public AudioClip jumpSound;

      void Start()
      {
          _rb2d = GetComponent<Rigidbody2D>();
          _audioSource = GetComponent<AudioSource>();
      }
  
      void Update()
      {
          _moveHorizontal = Input.GetAxis("Horizontal");

          if (topDown)
          {
              _moveVertical = Input.GetAxis("Vertical");   
              
              _movement = new Vector2 (_moveHorizontal, _moveVertical);
          }
          else
          {
              _movement = new Vector2 (_moveHorizontal, 0);
          }
          

          if (!topDown)
          {
              if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up"))
              {
                  if (_jumpsLeft > 0)
                  {
                      _jumpsLeft--;
                      Jump();    
                  }
              }
          }
          
          //animation
          if (turnAroundAnimation && GameManager.Instance.state == GameManager.State.InGame)
          {
              if (_moveHorizontal < 0)
              {
                  transform.rotation = Quaternion.Euler(0, 180, 0);
              } else if (_moveHorizontal > 0)
              {
                  transform.rotation = Quaternion.Euler(0, 0, 0);
              }
          }
      }

      private void OnCollisionEnter2D(Collision2D other)
      {
          if (!topDown)
          {
              if (other.gameObject.CompareTag("Ground"))
              {
                  _jumpsLeft = amountOfJumps;
              }
          }
      }

      private void FixedUpdate()
      {
          if (GameManager.Instance.state == GameManager.State.InGame)
          {
              _rb2d.AddForce (_movement * moveSpeed);
          }
      }

      private void Jump()
      {
          if (GameManager.Instance.state == GameManager.State.InGame)
          {
              _rb2d.AddForce(new Vector2(_rb2d.velocity.x, jumpForce));
              _audioSource.PlayOneShot(jumpSound);
          }
      }
}
