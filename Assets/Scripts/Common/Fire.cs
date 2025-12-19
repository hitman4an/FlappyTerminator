using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fire : MonoBehaviour, IPoolable<Fire>, IDamageable
{
    public event Action<Fire> DestroyObj;

    [SerializeField] private float _speed = 2f;
    [SerializeField] LayerMask _opponentLayerMask;

    private float _fireLifeTime = 3f;

    private Coroutine _coroutine;
    private Rigidbody2D _rigidBody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(DestroyAfterEndLifeTime());
    }

    private void Start()
    {
        _coroutine = StartCoroutine(DestroyAfterEndLifeTime());        
    }

    private void Update()
    {
        _rigidBody.velocity = _direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_opponentLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            DestroyObj?.Invoke(this);
            collision.GetComponent<IDamageable>().Die();
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void Die()
    {
        DestroyObj?.Invoke(this);
    }

    private IEnumerator DestroyAfterEndLifeTime()
    {
        yield return new WaitForSeconds(_fireLifeTime);

        DestroyObj?.Invoke(this);
    }
}
