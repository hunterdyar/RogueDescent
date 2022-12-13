using System;
using RogueDescent.Animation;
using RogueDescent.Attack;
using UnityEngine;

namespace RogueDescent.Feedbacks
{
	public class FlashSpriteOnHit : MonoBehaviour
	{
		//im not sure this is how I want to do feedbacks system.
		//but for now, single monobehaviours will work.

		private IAttackable _attackable;
		private SpriteRenderer _spriteRenderer;
		[Tooltip("In seconds. Use 0 for a single frame.")]
		[SerializeField] private float flashTime;
		[SerializeField] private Color flashColor;
		private void Awake()
		{
			_attackable = GetComponentInParent<IAttackable>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void OnEnable()
		{
			_attackable.OnHitTaken += OnHitTaken;
		}

		private void OnDisable()
		{
			_attackable.OnHitTaken -= OnHitTaken;
		}

		private void OnHitTaken(Impact impact)
		{
			_spriteRenderer.Flash(this,flashColor,flashTime);
		}

	}
}