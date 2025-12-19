using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FireSpawner))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 0.5f;
    
    private FireSpawner _spawner;
    private Coroutine _coroutine;

    private bool _canAttack;

    private void Awake()
    {
        _spawner = GetComponent<FireSpawner>();
    }

    private void OnEnable()
    {
        _canAttack = true;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void Attack()
    {
        if (_canAttack)
        {
            _canAttack = false;
            _spawner.CreateFire();
            _coroutine = StartCoroutine(WaitAttackCooldown());
        }
    }    

    private IEnumerator WaitAttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
    }
}
