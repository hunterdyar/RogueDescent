using System;
using UnityEngine;

namespace RogueDescent.Traps
{
	//A thing you can enter and exit
	public class StatusTrap : MonoBehaviour
	{
		[SerializeField] private Status.Status _trapStatus;
		[SerializeField] private bool _removeStatusOnExit;
		private Status.Status _activeStatus;
		private void OnTriggerEnter2D(Collider2D other)
		{
			var player = other.GetComponentInParent<Player>();
			if (player != null)
			{
				//clone status and add it.
				_activeStatus = Instantiate(_trapStatus);
				player.AddStatus(_activeStatus);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (_removeStatusOnExit)
			{
				var player = other.GetComponentInParent<Player>();
				if (player != null)
				{
					if (_activeStatus != null)
					{
						player.RemoveStatus(_activeStatus);
						_activeStatus = null;
					}

				}
			}
		}
	}
}