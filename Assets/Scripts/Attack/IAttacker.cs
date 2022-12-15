using UnityEngine;

namespace RogueDescent.Attack
{
	public interface IAttacker 
	{
		public Transform GetTransform()
		{
			return (this as MonoBehaviour)?.transform;
		}
	}
}