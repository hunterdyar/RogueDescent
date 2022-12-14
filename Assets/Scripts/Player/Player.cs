using System;
using System.Collections.Generic;
using System.Linq;
using RogueDescent.Abilities;
using RogueDescent.Attack;
using RogueDescent.Status;
using Unity.Collections;
using UnityEngine;

namespace RogueDescent
{
	/// <summary>
	/// Base player class that tracks stats/abilities/state and provides references to constant player-components (like movement, health, inventory)`
	/// </summary>
	public class Player : MonoBehaviour, IAttacker, IAffectedByStatus
	{
		public PlayerMovement PlayerMovement => _playerMovement;
		private PlayerMovement _playerMovement;

		public Stats BaseStats => _baseStats;
		[SerializeField] private Stats _baseStats;
		public Stats Stats => _activeStats;

		private Stats _activeStats;
		
		//todo: make observable lists
		//Track current abilities
		[SerializeField] private Ability[] Abilities;
		
		//Track current statuses
		[Readonly]
		[SerializeField] private List<Status.Status> _statuses;

		private void Awake()
		{
			//references
			_playerMovement = GetComponent<PlayerMovement>();
			_statuses = new List<Status.Status>();
			_activeStats = new Stats(_baseStats);
		}

		private void Start()
		{
			foreach (var abiility in Abilities)
			{
				abiility.SetPlayer(this);
				abiility.SetActive(true);
			}

			foreach (var abiility in Abilities)
			{
				abiility.OnGainAbility();
			}
		}

		void Update()
		{
			StatusTick();
			AbilityTick();
		}

		private void AbilityTick()
		{
			foreach(var ability in Abilities)
			{
				ability.Tick();
			}
		}

		//primary = 0, secondary = 1, etc.
		public bool TryActivateAbility(int slot)
		{
			if (slot < Abilities.Length && slot >= 0)
			{
				// Debug.Log($"Activating Ability {slot}");
				return Abilities[0].TryActivate();
			}
			
			return false;
		}

		private void StatusTick()
		{
			//reset and recalculate statuses
			
			//todo: I think the copyFrom is more efficient because of GC. 
			_activeStats.CopyFrom(_baseStats);
			// _activeStats = new Stats(_baseStats);

			foreach (var status in _statuses)
			{
				status.Process(_activeStats);
			}
		}

		public void AddStatus(Status.Status status, bool preventDuplicates = true)
		{
			if (preventDuplicates)
			{
				//This doesn't work, because we might have x "slow" statuses, and a 5% slow and 10% slow are different. We want unique scriptableObject assets, but we cant use instances because of cloning.
				//I thought this error was interesting.
				// var statusType = status.GetType();
				// if (_statuses.Any(x => x.GetType() == statusType))
				// {
				// 	return;
				// }

				if (_statuses.Any(x => x.name == status.name))
				{
					return;
				}
			}
			_statuses.Add(status);
		}
		
		public void RemoveStatus(Status.Status status)
		{
			_statuses.Remove(status);
		}

		private void OnDestroy()
		{
			foreach (var abiility in Abilities)
			{
				abiility.OnLoseAbility();
			}
		}
	}
}