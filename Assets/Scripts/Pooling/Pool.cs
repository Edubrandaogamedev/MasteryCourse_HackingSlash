using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
   private static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();
   private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();
   private PooledMonoBehaviour prefab;

   public static Pool GetPool(PooledMonoBehaviour prefab)
   {
      if (pools.ContainsKey(prefab))
         return pools[prefab];
      
      var pool = new GameObject("Pool-" + prefab.name).AddComponent<Pool>();
      pool.prefab = prefab;
      pools.Add(prefab,pool);
      return pool;
   }

   public T Get<T>() where T : PooledMonoBehaviour
   {
      if (objects.Count == 0)
      {
         GrownPool();
      }

      var pooledObject = objects.Dequeue();
      return pooledObject as T;
   }

   private void GrownPool()
   {
      for (var i = 0; i < prefab.InitialPoolSize; i++)
      {
         var pooledObject = Instantiate(prefab) as PooledMonoBehaviour;
         pooledObject.gameObject.name += " " + i;
         pooledObject.transform.SetParent(this.transform);
         pooledObject.OnReturnToPool += AddObjectToAvaiableQueue;
         pooledObject.gameObject.SetActive(false);
      }
   }

   private void AddObjectToAvaiableQueue(PooledMonoBehaviour pooledObject)
   {
      objects.Enqueue(pooledObject);
   }
}