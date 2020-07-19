using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnChunk : MonoBehaviour
{
    public GameObject[] chunks;
    public float timeForNextChunk;

    private void Start()
    {
        StartCoroutine(spawnChunk());
    }

    private IEnumerator spawnChunk()
    {
        while (true)
        {
            if (GameManager.Instance.state == GameManager.State.InGame)
            {
                
                Instantiate(chunks[Random.Range(0, chunks.Length)], transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(timeForNextChunk);
        }
    }
}
