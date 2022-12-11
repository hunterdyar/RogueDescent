using UnityEngine;

namespace RogueDescent.Abilities
{
	[CreateAssetMenu(fileName = "DashAbility", menuName = "Rogue/Abilities/Dash", order = 0)]
	public class DashAbility : Ability
	{
		[SerializeField] private float dashDistance;
		public override void OnActivate()
		{
			_player.PlayerMovement.Dash(_player.PlayerMovement.FacingDirection,dashDistance);
		}
	}
}