using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnChunk : MonoBehaviour
{
    public GameObject[] chunks;
    
    public void SpawnRandomChunk()
    {
        Instantiate(chunks[Random.Range(0, chunks.Length)], transform.position, Quaternion.identity);    
    }
}
