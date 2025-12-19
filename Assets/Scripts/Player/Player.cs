using System;
using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(CollisionDetector))]
[RequireComponent(typeof(Attacker))]
public class Player : MonoBehaviour, IDamageable
{
    public event Action GameOver;

    private PlayerMover _mover;
    private InputService _inputService;
    private CollisionDetector _handler;
    private Attacker _attacker;

    private bool _isActive = false;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _inputService = GetComponent<InputService>();
        _handler = GetComponent<CollisionDetector>();
        _attacker = GetComponent<Attacker>();
    }
    
    private void OnEnable()
    {
        _inputService.JumpButtonDown += Jump;
        _inputService.AttackButtonPressed += Attack;
        _handler.CollisionDetected += Die;
    }

    private void OnDisable()
    {
        _inputService.JumpButtonDown -= Jump;
        _inputService.AttackButtonPressed -= Attack;
        _handler.CollisionDetected -= Die;
    }

    private void Update()
    {
        if (_isActive)
        {
            _inputService.GetInput();
            _mover.Fall();
        }
    }

    public void Reset()
    {
        _isActive = true;
        _mover.Reset();
    }

    public void Die()
    {
        _isActive = false;
        GameOver?.Invoke();
    }

    private void Jump()
    {
        _mover.Jump();
    }

    private void Attack()
    {
        _attacker.Attack();
    }
}
