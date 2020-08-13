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
      public float trampolineJumpAgainTime;

      //Components
      [Header("Components")]
      private Rigidbody2D _rb2d;
      private AudioSource _audioSource;
      private Animator _animator;
      
      //Sound
      [Header("AudioClips")]
      public AudioClip trampolineSound;
      public AudioClip deadSound;

      void Start()
      {
          _rb2d = GetComponent<Rigidbody2D>();
          _audioSource = GetComponent<AudioSource>();
          _animator = GetComponent<Animator>();
      }
  
      void Update()
      {
            if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up"))
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
          
          //animation
          if (GameManager.Instance.state == GameManager.State.InGame)
          {
              _animator.speed = 1f;
              trampolineJumpAgainTime -= Time.deltaTime;
          }

          if (GameManager.Instance.state == GameManager.State.Pause)
          {
              _animator.speed = 0f;
          }

          if (gameObject.transform.position.x <= -0.5f)
          {
              recover();
          }
      }

      //Collision
      private void OnCollisionEnter2D(Collision2D other)
      {
          if (other.gameObject.CompareTag("Ground"))
          {
              _jumpsLeft = amountOfJumps;
              SetAnimation("Land");
          }

          if (other.gameObject.CompareTag("Spike"))
          {
              GameManager.Instance.PlayAgain();
              AudioManager.Instance.Stop();
              AudioManager.Instance.PlayAudioClip(deadSound);
              gameObject.SetActive(false);
          }
          
          if (other.gameObject.CompareTag("Trampoline"))
          {
              if (trampolineJumpAgainTime <= 0f)
              {
                  Jump();
                  trampolineJumpAgainTime = 0.1f;
              }

              if (AudioManager.Instance.sfx)
              {
                  AudioManager.Instance.PlayAudioClip(trampolineSound);    
              }
          }
      }
      
      private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.gameObject.CompareTag("Coin"))
          {
              GameManager.Instance.coinsCollected++;
              other.GetComponent<Animator>().SetBool("OnTrigger", true);
              other.gameObject.tag = "Untagged";
              AudioManager.Instance.PickUpCoin();
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
