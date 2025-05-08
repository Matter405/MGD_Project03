using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _flapStrength = 8f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _maxForwardRotation = 30f;
    [SerializeField] private float _maxDownwardRotation = -90f;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputHandler _input;
    private bool _isTouching;

    public event Action PlayerDied;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
        //_input = FindAnyObjectByType<GameController>().GetComponent<InputHandler>();
        _isTouching = false;
    }

    private void Update()
    {
        if(_isTouching)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

            // Apply an instant upward force
            _rb.AddForce(Vector2.up * _flapStrength, ForceMode2D.Impulse);
            _isTouching = false;
        }
    }

    private void FixedUpdate()
    {
        float targetRotation = _rb.linearVelocityY * _rotationSpeed;

        targetRotation = Mathf.Clamp(targetRotation, _maxDownwardRotation, _maxForwardRotation);
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        PlayerDied?.Invoke();
    }

    private void OnTouchPerformed(Vector2 position)
    {
        _isTouching = true;
    }

    private void OnEnable()
    {
        _input.TouchStarted += OnTouchPerformed;
    }

    private void OnDisable()
    {
        _input.TouchStarted -= OnTouchPerformed;
    }
}
