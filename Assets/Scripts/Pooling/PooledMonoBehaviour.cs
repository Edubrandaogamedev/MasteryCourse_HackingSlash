using System;
using System.Collections;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField] private int initialPoolSize = 50;
    
    public event Action<PooledMonoBehaviour> OnReturnToPool;
    public int InitialPoolSize => initialPoolSize;
    
    protected  virtual void OnDisable()
    {
        OnReturnToPool?.Invoke(this);
    }

    private IEnumerator ReturnToPoolAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    protected void ReturnToPool(float delay = 0)
    {
        StartCoroutine(ReturnToPoolAfterSeconds(delay));
    }
    public T Get<T>(bool enable = true) where T : PooledMonoBehaviour
    {
        var pool = Pool.GetPool(this);
        var pooledObject = pool.Get<T>();
        pooledObject.gameObject.SetActive(enable);
        return pooledObject;
    }

    public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour
    {
        var pooledOject = Get<T>();
        pooledOject.transform.position = position;
        pooledOject.transform.rotation = rotation;
        return pooledOject;
    }
}