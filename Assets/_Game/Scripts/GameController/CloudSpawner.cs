using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 10f;
    [SerializeField] private GameObject _cloud;
    [SerializeField] private Transform _cloudSpawnLocation;

    private float _timer;

    private void Start()
    {
        Spawn();
        Debug.Log("First Cloud");
    }

    private void Update()
    {
        if(_timer > _maxTime)
        {
            Spawn();
            _timer = 0;
            Debug.Log("Spawn Cloud");
        }

        _timer += Time.deltaTime;
    }

    private void Spawn()
    {
        Vector3 spawnPos = _cloudSpawnLocation.position +
            new Vector3(0, Random.Range(-_heightRange, _heightRange));
        GameObject pipe = Instantiate(_cloud, spawnPos, Quaternion.identity);

        Destroy(pipe, 360f);
    }

}
