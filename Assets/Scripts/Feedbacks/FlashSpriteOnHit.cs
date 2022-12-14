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
		//todo: Add repeat flashing option, but actually make it a "Procedural Sprite Animation" struct and custom property drawer, maybe.
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

		#region Editor Validation
		
		private void OnValidate()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
#if UNITY_EDITOR

			if (!_spriteRenderer.sharedMaterial.HasProperty(SRProcAnim.ColorProp))
			{
				Debug.LogWarning("Sprite Renderer Needs Rogue Character material in order for sprite flashing to work! No Color Prop", this);
			}

			if (!_spriteRenderer.sharedMaterial.HasProperty(SRProcAnim.IgnoreTexture))
			{
				Debug.LogWarning("Sprite Renderer Needs Rogue Character material in order for sprite flashing to work! No 'Ignore Texture' Prop", this);
			}

#endif

		}

		#endregion
	}
}