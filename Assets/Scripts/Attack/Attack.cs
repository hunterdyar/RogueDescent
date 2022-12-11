using UnityEngine;

namespace RogueDescent.Attack
{
	public class Attack
	{
		//todo: some attacks can or cannot be critical.
		public AttackState State;
		public float damage;
		public Collider2D attackArea;
		public Vector3 SourcePosition;
		public IAttacker Attacker;
		public Status.Status InflictStatus; //poision? slow?
	}
}