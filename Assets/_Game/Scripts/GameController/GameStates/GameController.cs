using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] public float _setupWaitTime = .2f;

    [Header("Dependencies")]
    [SerializeField] public Unit _playerUnitPrefab;
    [SerializeField] public Transform _playerUnitSpawnLocation;
    [SerializeField] public UnitSpawner _unitSpawner;
    [SerializeField] public InputHandler _input;
    [SerializeField] public GameHUDController _hudController;
    [SerializeField] public PipeSpawner _pipeSpawner;
    [SerializeField] public Transform _pipeSpawnLocation;
    [SerializeField] public CloudSpawner _cloudSpawner;
    [SerializeField] public Transform _cloudSpawnLocation;

    public float SetupWaitTime => _setupWaitTime;

    public Unit PlayerUnitPrefab => _playerUnitPrefab;
    public Transform PlayerUnitSpawnLocation => _playerUnitSpawnLocation;
    public UnitSpawner UnitSpawner => _unitSpawner;
    public InputHandler Input => _input;
    public GameHUDController HUDController => _hudController;
    public PipeSpawner PipeSpawner => _pipeSpawner;
    public Transform PipeSpawnLocation => _pipeSpawnLocation;
    public CloudSpawner CloudSpawner => _cloudSpawner;
    public Transform CloudSpawnLocation => _cloudSpawnLocation;
}
