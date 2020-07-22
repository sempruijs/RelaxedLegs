using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnviorment : MonoBehaviour
{
    public float speed;
    public Vector3 point;
    
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
        }
    }
}
