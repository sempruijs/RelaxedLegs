using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnChunk : MonoBehaviour
{
    public GameObject[] chunks;
    public GameObject emptyChunk;
    public float timeForNextChunk;

    private void Start()
    {
    }

    // private IEnumerator spawnChunk()
    // {
    //     while (true)
    //     {
    //         if (GameManager.Instance.state == GameManager.State.InGame)
    //         {
    //             
    //             Instantiate(chunks[Random.Range(0, chunks.Length)], transform.position, Quaternion.identity);
    //         }
    //         yield return new WaitForSeconds(timeForNextChunk);
    //     }
    // }

    public void SpawnRandomChunk()
    {
        Instantiate(chunks[Random.Range(0, chunks.Length)], transform.position, Quaternion.identity);    
    }

    public void SpawnChosen(GameObject chunk)
    {
        Instantiate(chunk, transform.position, Quaternion.identity);
    }
}
