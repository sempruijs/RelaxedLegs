using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public AudioClip playWithSoundClip;
    public GameObject pointA;
    public GameObject pointB;
    public float minimumTime;
    public float maximumTime;

    public GameObject[] powerUps;

    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
    public IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            if (GameManager.Instance.state == GameManager.State.InGame)
            {
                Vector3 spawnPosition =
                    new Vector3(Random.Range(pointA.transform.position.x, pointB.transform.position.x),
                        Random.Range(pointA.transform.position.y, pointB.transform.position.y), -1f);
                
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPosition, Quaternion.identity);
                AudioManager.Instance.PlayAudioClip(playWithSoundClip);
            }
            yield return new WaitForSeconds(Random.Range(minimumTime, maximumTime));
        }
    }

}