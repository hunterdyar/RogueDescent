using Unity.VisualScripting;
using UnityEngine;

namespace RogueDescent.Attack
{
	public interface IAttackable 
	{
		public void TakeHit(Impact impact);
		
		//who am i? Used to prevent me from hitting myself. Null for default
		public IAttacker GetMyAttacker()
		{
			return null;
		}
	}
}