using UnityEngine;

public class PipeIncreaseScore : MonoBehaviour
{

    public GameController _controller;

    private void Awake()
    {
        _controller = FindFirstObjectByType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _controller.HUDController.IncreaseScores(1);
            _controller.HUDController.DisplayScores();
        }
    }
}
