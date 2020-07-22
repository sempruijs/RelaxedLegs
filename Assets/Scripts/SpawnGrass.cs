using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnGrass : MonoBehaviour
{
    public GameObject[] grass;
    public float timeForNextGrass;

    private void Start()
    {
        StartCoroutine(GrassCoroutine());
    }

    private IEnumerator GrassCoroutine()
    {
        while (true)
        {
            if (GameManager.Instance.state == GameManager.State.InGame)
            {
                
                Instantiate(grass[Random.Range(0, grass.Length)], transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(timeForNextGrass);
        }
    }
}
