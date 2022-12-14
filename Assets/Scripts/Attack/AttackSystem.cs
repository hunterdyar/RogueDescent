using System;
using UnityEngine;

namespace RogueDescent.Attack
{
	/// <summary>
	/// Anybody attacking
	/// </summary>
	[CreateAssetMenu(fileName = "AttackSystem", menuName = "Rogue/Managers/Attack System", order = 0)]
	public class AttackSystem : ScriptableObject
	{
		//events
		public Action<Impact> OnImpact;
		
		public ContactFilter2D AttackFilter;
		private Collider2D[] _hits = new Collider2D[20]; 
		//During whatever animation or projectile things are happening, we eventually get this called. An attack is a splash that happens on a single frame. 
		public void Attack(Attack attack)
		{
			//Checks the collider in attack for overlaps that have IAttackable
			int hitCount = attack.attackArea.OverlapCollider(AttackFilter, _hits);
			if (hitCount > 0)
			{
				for (int i = 0; i < hitCount; i++)
				{
					var victim = _hits[i].GetComponentInParent<IAttackable>();
					if (victim != null)
					{
						if (victim.GetMyAttacker() != attack.Attacker)
						{
							//halfway between attacker (collider) and position (collider)
							//really we would want the collision point, but colliders are overlap flashes - check overlaps, so no collision point.
							//maybe get nearest point on attack shape perimeter?
							//maybe pass "impact position" get off to the attack to overridable attack code, some set of "by them, by me, at center".
							
							Vector3 position = Vector3.Lerp(_hits[i].transform.position, attack.Attacker.GetTransform().position, 0.2f);
							var impact = new Impact(attack, victim, position, CalculateDamage(attack, victim));
							victim.TakeHit(impact);
							//fire impact event (UI system listens to this)
							OnImpact?.Invoke(impact);
						}
					}
				}
			}
			else
			{
				//no hits
				attack.State = AttackState.Missed;
			}
		}

		private float CalculateDamage(Attack attack, IAttackable victim)
		{
			//Get if critical hit

			//get if strong/weak to attack type

			return attack.damage;
		}
	}
}