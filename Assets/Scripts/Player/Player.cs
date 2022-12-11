using System;
using System.Collections.Generic;
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
	public class Player : MonoBehaviour, IAttacker
	{
		public PlayerMovement PlayerMovement => _playerMovement;
		private PlayerMovement _playerMovement;

		public Stats BaseStats => _baseStats;
		[SerializeField] private Stats _baseStats;
		public Stats Stats => _activeStats;

		private Stats _activeStats;
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

		public void AddStatus(Status.Status status)
		{
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