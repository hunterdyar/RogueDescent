using UnityEngine;

namespace RogueDescent.Status
{
	public class Status : ScriptableObject
	{
		private float statusTimer = 1;
		private bool activated = true;
		
		public void Process(Stats stats)
		{
			if (activated && statusTimer > 0)
			{
				DoProcess(stats);
			}
		}

		protected virtual void DoProcess(Stats stats)
		{
			
		}
	}
}