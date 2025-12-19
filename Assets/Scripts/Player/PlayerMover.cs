using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidBody;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        Reset();
    }

    public void Jump()
    {
        _animator.SetJump();
        _rigidBody.velocity = new Vector2(_speed, _jumpForce);
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _rigidBody.velocity = Vector2.zero;
    }
}
