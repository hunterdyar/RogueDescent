using System;
using System.Security.Cryptography;
using UnityEngine;

namespace RogueDescent.Attack
{
	//Generic health class.
	public class Health : MonoBehaviour, IAttackable
	{
		[SerializeField] private float maxHealth;
		private IAttacker _attacker;
		private void Awake()
		{
			_attacker = GetComponent<IAttacker>();
		}

		public IAttacker GetMyAttacker()
		{
			return _attacker;
		}

		public Health(IAttacker attacker)
		{
			_attacker = attacker;
		}

		public void TakeHit(Impact impact)
		{
			maxHealth -= impact.RealDamage;
			//flash animation
			if (maxHealth <= 0)
			{
				Destroy(gameObject);
			}
		}
	
		
	}
}