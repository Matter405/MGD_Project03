using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;

    private void Update()
    {
        transform.position += Vector3.left * Random.Range(_speed, _speed + 3f) * Time.deltaTime;
    }
}
