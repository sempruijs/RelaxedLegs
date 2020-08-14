using UnityEngine;

public class PlayAnimationCollideWithPlayer : MonoBehaviour
{
    public Animator animator;
    public string setBool;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool(setBool, true);    
        }
    }
}
