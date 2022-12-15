using System.Collections.Generic;
using UnityEngine;

namespace RogueDescent.Pooling
{
	//
	/// <summary>
	/// Manages a pool of objects, assuming they are can be instantiated from a prefab.
	/// _pooledObjectPrefab needs to be set to a Prefab that has a component of type PooledObject, which is an abstract class.
	/// </summary>
	public class GameObjectPool : MonoBehaviour
	{
		//Creating a delegate is like creating a type which can save functions.
		//the pooledObject has a "PoolEvent ReturnToPool" variable (of type this delegate), and we set that to our ReturnObjectToPool function.
		public delegate void PoolEvent(PooledObject o);

		public PooledObject PooledObjectPrefab => _pooledOjectPrefab;
		[SerializeField] private PooledObject _pooledOjectPrefab;

		//todo: convert to stacks? RemoveAt vs. Pop performance?

		private readonly List<PooledObject> _pool = new List<PooledObject>();
		public List<PooledObject> GetActiveObjectsInPool => _active;
		private readonly List<PooledObject> _active = new List<PooledObject>();
		public GameObject GetObject(Vector3 position, Quaternion rotation, Transform parent)
		{
			for (var i = 0; i < _pool.Count; i++)
			{
				var pooled = _pool[i];
				pooled.gameObject.SetActive(true);
				pooled.ResetAsNew(position, rotation, parent);
				_pool.RemoveAt(i);
				_active.Add(pooled);
				return pooled.gameObject;
			}

			//Create new
			PooledObject po = Instantiate(_pooledOjectPrefab, position, rotation, parent);
			po.ReturnToPool = ReturnObjectToPool;
			_active.Add(po);
			return po.gameObject;
		}

		public T GetObject<T>(Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
		{
			//todo: factor this method out
			for (var i = 0; i < _pool.Count; i++)
			{
				var pooled = _pool[i];
				pooled.gameObject.SetActive(true);
				pooled.ResetAsNew(position, rotation, parent);
				_pool.RemoveAt(i);
				_active.Add(pooled);
				return pooled.gameObject.GetComponent<T>();
			}

			//Create new
			PooledObject po = Instantiate(_pooledOjectPrefab, position, rotation, parent);
			po.ReturnToPool = ReturnObjectToPool;
			_active.Add(po);
			//cheaper than a GetComponent call
			if (po is T pt)
			{
				return pt;
			}
			else
			{
				Debug.LogWarning($"Pool unable to get object of type {typeof(T).Name}. Check Prefab in pool",this);
				return null;
			}
		}

		public T GetObject<T>() where T : MonoBehaviour
		{
			return GetObject<T>(_pooledOjectPrefab.transform.position, _pooledOjectPrefab.transform.rotation, null);
		}

		public T GetObject<T>(Transform parent) where T : MonoBehaviour
		{
			return GetObject<T>(_pooledOjectPrefab.transform.position, _pooledOjectPrefab.transform.rotation, parent);
		}
		//I don't like single letter variables, but "object" is very reserved by C#. "o" for object isn't as common as i for index/iterator or e for event, but it's standard enough. 
		public void ReturnObjectToPool(PooledObject o)
		{
			o.gameObject.SetActive(false);
			_active.Remove(o);
			_pool.Add(o);
		}
	}
}