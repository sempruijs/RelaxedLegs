using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectInTimeFrame : MonoBehaviour
{
    public float minimumTime;
    public float maximumTime;
    public AudioClip playWithSoundClip;
    public GameObject prefab;
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
                Instantiate(prefab, transform.position, Quaternion.identity);
                AudioManager.Instance.PlayAudioClip(playWithSoundClip);
            }
            yield return new WaitForSeconds(Random.Range(minimumTime, maximumTime));
        }
    }
}
