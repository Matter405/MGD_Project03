using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 10f;
    [SerializeField] private GameObject _pipe;
    [SerializeField] private Transform _pipeSpawnLocation;

    private float _timer;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        if(_timer > _maxTime)
        {
            Spawn();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void Spawn()
    {
        Vector3 spawnPos = _pipeSpawnLocation.position +
            new Vector3(0, Random.Range(-_heightRange, _heightRange));
        GameObject pipe = Instantiate(_pipe, spawnPos, Quaternion.identity);

        Destroy(pipe, 10f);
    }

}
