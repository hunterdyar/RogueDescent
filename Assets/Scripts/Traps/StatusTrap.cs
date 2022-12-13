using System;
using RogueDescent.Status;
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
			//Todo: Change to "IAffectedByStatus" or such interface.
			var target = other.GetComponentInParent<IAffectedByStatus>();
			if (target != null)
			{
				//clone status and add it.
				if(_activeStatus == null)
				{
					_activeStatus = Instantiate(_trapStatus);
				}
				
				target.AddStatus(_activeStatus, true);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (_removeStatusOnExit)
			{
				var target = other.GetComponentInParent<IAffectedByStatus>();
				if (target != null)
				{
					if (_activeStatus != null)
					{
						target.RemoveStatus(_activeStatus);
						_activeStatus = null;
					}

				}
			}
		}
	}
}