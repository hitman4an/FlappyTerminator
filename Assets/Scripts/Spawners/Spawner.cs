using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T: MonoBehaviour, IObjectable<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int PoolCapacity = 5;
    [SerializeField] protected int PoolMaxSize = 5;

    public event Action ObjectCreated;
    public event Action ObjectReceived;
    public event Action<T> ObjectReleased;

    protected ObjectPool<T> Pool;
    protected bool IsActive = true;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: () => CreateObject(Prefab),
        actionOnGet: (obj) => ClearObjectTransform(obj),
        actionOnRelease: (obj) => ReleaseObjectAction(obj),
        actionOnDestroy: (obj) => Destroy(obj.gameObject),
        collectionCheck: true,
        defaultCapacity: PoolCapacity,
        maxSize: PoolMaxSize);
    }

    protected T CreateObject(T prefab)
    {
        T obj = Instantiate(prefab);

        ObjectCreated?.Invoke();

        return obj;
    }

    protected virtual void ClearObjectTransform(T obj)
    {
        ObjectReceived?.Invoke();
    }

    protected void ReleaseObjectAction(T obj)
    {
        obj.gameObject.SetActive(false);
        ObjectReleased?.Invoke(obj);
    }

    protected void ReleaseObject(T obj)
    {
        obj.DestroyObj -= ReleaseObject;
        Pool.Release(obj);
    }
}
