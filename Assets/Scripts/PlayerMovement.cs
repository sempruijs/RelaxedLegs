using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
      [Header("Speed")]
      public float jumpForce;
      
      [Header("Data")]
      public int amountOfJumps;
      private int _jumpsLeft;
      public float trampolineJumpAgainTime;

      [Header("Components")]
      private Rigidbody2D _rb2d;
      private Animator _animator;
      
      [Header("AudioClips")]
      public AudioClip trampolineSound;
      public AudioClip deadSound;

      //-------------- Start --------------
      void Start()
      {
          _rb2d = GetComponent<Rigidbody2D>();
          _animator = GetComponent<Animator>();
      }
  
      //-------------- Update --------------
      void Update()
      {
           // handel movement
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
              
           if (gameObject.transform.position.x <= -1.5f)
           { 
               Recover();
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
      }

      //-------------- Collision --------------
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

      
      //-------------- Movement --------------
      
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

      private void Recover()
      {
          _rb2d.AddForce(new Vector2(300f, 0f));
      }
      
      public void GoDown(bool x)
      {
          _animator.SetBool("Down", x);
      }
      
      //-------------- Animation --------------
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
}
