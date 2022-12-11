using UnityEngine;

namespace RogueDescent.Attack
{
	//when an attack lands it turns into an impact. Impacts are processed by the attacker attackee.
	public struct Impact
	{
		public Attack attack;
		public IAttacker Attacker => attack.Attacker;
		public IAttackable Victim;
		public float RealDamage;
		public Status.Status Status => attack.InflictStatus;
		public Vector3 ImpactLocation;//world space.

		public Impact(Attack attack, IAttackable victim, Vector3 impactLocation, float realRealDamage)
		{
			this.attack = attack;
			Victim = victim;
			RealDamage = realRealDamage;
			ImpactLocation = impactLocation;
		}
	}
}