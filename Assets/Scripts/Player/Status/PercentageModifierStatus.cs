using UnityEngine;

namespace RogueDescent.Status
{
	[CreateAssetMenu(fileName = "Percentage", menuName = "Rogue/Status/Percentage Modifier", order = 0)]
	public class PercentageModifierStatus : Status
	{
		//todo: do this as serialized stats? i figure nah cus custom editor.
		[SerializeField] private float _speedPercentage;
		[SerializeField] private float _armorPercentage;
		protected override void DoProcess(Stats stats)
		{
			stats.speed = stats.speed*_speedPercentage;
			stats.armor = stats.armor * _armorPercentage;
		}
	}
}