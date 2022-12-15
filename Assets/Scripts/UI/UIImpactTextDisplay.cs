using System;
using RogueDescent.Attack;
using RogueDescent.Pooling;
using UnityEngine;

namespace RogueDescent.UI
{
	public class UIImpactTextDisplay : MonoBehaviour
	{
		//Listen for Impact events.
		[SerializeField] private AttackSystem _attackSystem;
		[SerializeField] private GameObjectPool _UIImpactTextPool;
		private void OnEnable()
		{
			_attackSystem.OnImpact += OnImpact;
		}

		private void OnDisable()
		{
			_attackSystem.OnImpact -= OnImpact;
		}

		private void OnImpact(Impact impact)
		{
			var impactText = _UIImpactTextPool.GetObject<UIImpactText>(transform);
			impactText.SetImpact(impact);
		}
	}
}