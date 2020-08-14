using UnityEngine;

public class MoveEnviorment : MonoBehaviour
{
    public float speed;
    private float _normalSpeed;
    public Vector3 point;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _normalSpeed = speed;
    }
    
    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.InGame)
        {
            float step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point, step);
            
            if (transform.position.x <= point.x)
            {
                Destroy(gameObject);
            }
            
            if (_player.transform.position.x <= -0.5f)
            {
                speed = _normalSpeed * 0.66667f;
            } 
            else
            {
                speed = _normalSpeed;
            }
        }
    }
}
