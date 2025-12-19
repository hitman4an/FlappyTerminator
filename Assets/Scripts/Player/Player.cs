using System;
using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Attacker))]
public class Player : MonoBehaviour, IOpponentable
{
    public event Action GameOver;

    private PlayerMover _mover;
    private InputService _inputService;
    private CollisionHandler _handler;
    private Attacker _attacker;

    private bool _isActive = false;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _inputService = GetComponent<InputService>();
        _handler = GetComponent<CollisionHandler>();
        _attacker = GetComponent<Attacker>();
    }
    
    private void OnEnable()
    {
        _inputService.JumpBtnDown += Jump;
        _inputService.AttackBtnPressed += Attack;
        _handler.CollisionDetected += Dead;
    }

    private void OnDisable()
    {
        _inputService.JumpBtnDown -= Jump;
        _inputService.AttackBtnPressed -= Attack;
        _handler.CollisionDetected -= Dead;
    }

    private void Update()
    {
        if (_isActive)
        {
            _inputService.GetInput();
        }
    }

    public void Reset()
    {
        _isActive = true;
        _mover.Reset();
    }

    public void Dead()
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
