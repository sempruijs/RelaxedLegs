using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float speed;
    public Vector3 point;

    private SpawnChunk _spawnChunk;
    private GameObject _player;

    private void Start()
    {
        _spawnChunk = GameObject.FindWithTag("ChunkSpawner").GetComponent<SpawnChunk>();
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.InGame)
        {
            float step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point, step);
            
            if (transform.position.x <= point.x)
            {
                _spawnChunk.SpawnRandomChunk();
                Destroy(gameObject);
            }

            if (_player.transform.position.x <= -0.5f)
            {
                speed = 4f;
            } 
            else
            {
                speed = 6f;
            }
        }
    }
}
