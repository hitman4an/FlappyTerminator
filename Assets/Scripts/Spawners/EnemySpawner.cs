using System.Collections;
using UnityEngine;


public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private Player _player;
    [SerializeField] Game _game;

    private float _minYPosition = -4;
    private float _maxYPosition = 2.5f;
    private float _playerOffset = 16;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _game.GameStarted += Activate;
        _game.GameEnded += Deactivate;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(GetObject());

        _game.GameStarted -= Activate;
        _game.GameEnded -= Deactivate;
    }

    protected override void ClearObjectTransform(Enemy obj)
    {
        obj.transform.position = new Vector2(_player.transform.position.x + _playerOffset, Random.Range(_minYPosition, _maxYPosition));
        obj.gameObject.SetActive(true);

        base.ClearObjectTransform(obj);
    }

    private IEnumerator GetObject()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (IsActive)
        {
            Enemy obj = Pool.Get();

            obj.DestroyObj += ReleaseObject;

            yield return wait;
        }
    }

    private void Activate()
    {
        _coroutine = StartCoroutine(GetObject());
    }

    private void Deactivate()
    {
        if (_coroutine != null)
            StopCoroutine(GetObject());
    }
}
