using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float speed;
    public Vector3 point;

    private bool _shouldBeMoving = true;
    private SpawnChunk _spawnChunk;

    private void Start()
    {
        _spawnChunk = GameObject.FindWithTag("ChunkSpawner").GetComponent<SpawnChunk>();
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
        }
    }
}
