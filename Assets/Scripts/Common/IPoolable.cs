using System;
using UnityEngine;

public interface IPoolable<T> where T: MonoBehaviour
{
    public event Action<T> DestroyObj;
}
