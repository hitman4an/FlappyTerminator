using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Enemy : MonoBehaviour, IPoolable<Enemy>, IDamageable
{
    public event Action<Enemy> DestroyObj;

    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private float _lifeTime = 30f;

    private Attacker _attacker;
    private Coroutine _attack;
    private Coroutine _destroy;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _attack = StartCoroutine(Attack());
        _destroy = StartCoroutine(DestroyAfterLifeTime());
    }

    private void OnDisable()
    {
        if (_destroy != null)
            StopCoroutine(_destroy);

        if (_attack != null)
            StopCoroutine(_attack);
    }

    private IEnumerator Attack()
    {
        var wait = new WaitForSeconds(_attackCooldown);

        while (enabled)
        {
            yield return wait;

            _attacker.Attack();
        }
    }
    private IEnumerator DestroyAfterLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        DestroyObj?.Invoke(this);
    }

    public void Die()
    {
        DestroyObj?.Invoke(this);
    }
}
