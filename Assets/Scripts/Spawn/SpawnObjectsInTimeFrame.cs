using System.Collections;
using UnityEngine;

public class SpawnObjectsInTimeFrame : MonoBehaviour
{
    public float minimumTime;
    public float maximumTime;
    public GameObject[] prefabs;
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
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(minimumTime, maximumTime));
        }
    }
}