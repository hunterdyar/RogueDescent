using UnityEngine;

namespace RogueDescent.Abilities
{
	//An ability is something you can _do_.
	public abstract class Ability : ScriptableObject
	{
		//Configuration
		[SerializeField] private float baseCooldown;
		
		protected Player _player;
		private bool _active;

		//do we want to set hasCooldown ot infer it? Once we bother to do a custom editor script, a bool that shows/hides the float is more reasonable.
		private bool hasCooldown => baseCooldown > 0;
		private float _cooldown;

		public void SetPlayer(Player player)
		{
			_player = player;
		}
		
		/// <summary>
		/// Tick is called on all equipped abilities in the update loop.
		/// </summary>
		public virtual void Tick()
		{
			if (hasCooldown && _cooldown > 0)
			{
				_cooldown -= Time.deltaTime;
			}
		}

		private float GetActualCooldownTime()
		{
			return _player.Stats.cooldownModifier*_cooldown;
		}
		/// <summary>
		/// Activate is called by the players input. 
		/// </summary>
		public virtual bool TryActivate()
		{
			//activate. Then. reset cooldown.
			if (!_active)
			{
				return false;
			}

			if (_cooldown > 0)
			{
				return false;
			}
			
			OnActivate();
			_cooldown = GetActualCooldownTime();
			return true;
		}

		public virtual void OnGainAbility()
		{
			
		}

		public virtual void OnLoseAbility()
		{
			
		}

		/// <summary>
		/// OnActivate executes the ability after validation and ability checks.
		/// </summary>
		public abstract void OnActivate();

		public void SetActive(bool active)
		{
			_active = active;
		}
	}
}