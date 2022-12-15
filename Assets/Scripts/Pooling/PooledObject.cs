using UnityEngine;

namespace RogueDescent.Pooling
{
	public class PooledObject : MonoBehaviour
		{
			public GameObjectPool.PoolEvent ReturnToPool;
			public virtual void ResetAsNew(Vector3 position, Quaternion rotation, Transform parent)
			{
				transform.position = position;
				transform.rotation = rotation;
				transform.SetParent(parent);
			}

			//conveninence wrapper to make guessing what function to call easier when looking through options.
			protected void ReturnSelfToPool()
			{
				ReturnToPool(this);
			}
		}
	}
