using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] public float _setupWaitTime = 3f;

    [Header("Dependencies")]
    [SerializeField] public Unit _playerUnitPrefab;
    [SerializeField] public Transform _playerUnitSpawnLocation;
    [SerializeField] public UnitSpawner _unitSpawner;
    [SerializeField] public InputHandler _input;
    [SerializeField] public GameHUDController _hudController;

    public float SetupWaitTime => _setupWaitTime;

    public Unit PlayerUnitPrefab => _playerUnitPrefab;
    public Transform PlayerUnitSpawnLocation => _playerUnitSpawnLocation;
    public UnitSpawner UnitSpawner => _unitSpawner;
    public InputHandler Input => _input;
    public GameHUDController HUDController => _hudController;
}
