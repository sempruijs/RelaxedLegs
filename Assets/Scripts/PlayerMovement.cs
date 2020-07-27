using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
      //Input
      [Header("Input")]
      private float _moveHorizontal;
      private float _moveVertical;
      private Vector2 _movement;
      
      //Speed
      [Header("Speed")]
      public float moveSpeed;
      public float jumpForce;
      
      //Data
      [Header("Data")]
      public int amountOfJumps;
      private int _jumpsLeft;
      public bool turnAroundAnimation;
      public bool topDown;

      //Components
      [Header("Components")]
      private Rigidbody2D _rb2d;
      private AudioSource _audioSource;
      private Animator _animator;
      
      //Sound
      [Header("AudioClips")]
      public AudioClip jumpSound;
      public AudioClip trampolineSound;
      public AudioClip deadSound;
      public AudioClip pickUpCoin;

      void Start()
      {
          _rb2d = GetComponent<Rigidbody2D>();
          _audioSource = GetComponent<AudioSource>();
          _animator = GetComponent<Animator>();
      }
  
      void Update()
      {
          // _moveHorizontal = Input.GetAxis("Horizontal");

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
              if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space"))
              {
                 ActivateJump();
              }

              if (Input.GetKeyDown("down"))
              {
                  GoDown(true);
              }

              if (Input.GetKeyUp("down"))
              {
                  GoDown(false);
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

          if (gameObject.transform.position.x <= -0.5f)
          {
              recover();
          }
      }

      //Collision
      private void OnCollisionEnter2D(Collision2D other)
      {
          if (!topDown)
          {
              if (other.gameObject.CompareTag("Ground"))
              {
                  _jumpsLeft = amountOfJumps;
                  SetAnimation("Land");
              }

              if (other.gameObject.CompareTag("Spike"))
              {
                  GameManager.Instance.PlayAgain();
                  AudioManager.Instance.PlayAudioClip(deadSound);
                  gameObject.SetActive(false);
              }
              
              if (other.gameObject.CompareTag("Trampoline"))
              {
                  Jump();
                  AudioManager.Instance.PlayAudioClip(trampolineSound);
              }
          }
      }

      // private void OnCollisionExit2D(Collision2D other)
      // {
      //     if (other.gameObject.CompareTag("Ground"))
      //     {
      //         SetAnimation("Jump");
      //     }
      // }

      // private void FixedUpdate()
      // {
      //     if (GameManager.Instance.state == GameManager.State.InGame)
      //     {
      //         _rb2d.AddForce (_movement * moveSpeed);
      //     }
      // }

      private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.gameObject.CompareTag("Coin"))
          {
              GameManager.Instance.coinsCollected++;
              other.GetComponent<Animator>().SetBool("OnTrigger", true);
              other.gameObject.tag = "Untagged";
              AudioManager.Instance.PlayAudioClip(pickUpCoin);
          }
      }

      private void Jump()
      {
          if (GameManager.Instance.state == GameManager.State.InGame)
          {
              _rb2d.AddForce(new Vector2(_rb2d.velocity.x, jumpForce));
          }
      }

      public void ActivateJump()
      {
          if (_jumpsLeft > 0)
          {
              _jumpsLeft--;
              Jump();
              // _audioSource.PlayOneShot(jumpSound);
              SetAnimation("Jump");
          }
      }

      public void recover()
      {
          _rb2d.AddForce(new Vector2(500.0f, 0f));
      }
      
      //Animation
      public void SetAnimation(string animation)
      {
          switch (animation) 
          {
              case "Jump":
                  _animator.SetBool("IsJumping", true);
                  _animator.SetBool("IsLanding", false);
                  break;
              case "Land":
                  _animator.SetBool("IsLanding", true);
                  _animator.SetBool("IsJumping", false);
                  break;
          }
      }
      
      public void GoDown(bool x)
      {
        _animator.SetBool("Down", x);
      }
}
