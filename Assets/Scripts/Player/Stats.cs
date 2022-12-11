namespace RogueDescent
{
	[System.Serializable]
	//Active Stats.
	public class Stats
	{
		public float armor = 0;
		public float speed = 5;
		public float cooldownModifier = 1;
		public Stats(Stats copy)
		{
			CopyFrom(copy);
		}

		public void CopyFrom(Stats other)
		{
			armor = other.armor;
			speed = other.speed;
			cooldownModifier = other.cooldownModifier;
		}
	}
}