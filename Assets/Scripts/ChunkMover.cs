using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float speed;
    private float _normalSpeed;
    public Vector3 point;

    private SpawnChunk _spawnChunk;
    private GameObject _player;

    private void Start()
    {
        _spawnChunk = GameObject.FindWithTag("ChunkSpawner").GetComponent<SpawnChunk>();
        _player = GameObject.FindWithTag("Player");
        _normalSpeed = speed;
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

            if (_player.transform.position.x <= -1.5f)
            {
                speed = _normalSpeed * 0.66667f;
            } 
            else
            {
                speed = _normalSpeed;
            }
        }
    }
}
