using UnityEngine;

namespace RogueDescent.Attack
{
	public class Attack
	{
		public AttackState State;
		public float damage;
		public Collider2D attackArea;
		public Vector3 SourcePosition;
		public IAttacker Attacker;
		public Status.Status InflictStatus; //poision? slow?
	}
}