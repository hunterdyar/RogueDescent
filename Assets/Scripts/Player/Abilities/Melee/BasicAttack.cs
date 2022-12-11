using System.Collections;
using RogueDescent.Attack;
using UnityEditor.Sprites;
using UnityEngine;

namespace RogueDescent.Abilities.Melee
{
	[CreateAssetMenu(fileName = "Bop Attack", menuName = "Rogue/Abilities/Bop", order = 0)]
	public class BasicAttack : Ability
	{
		[SerializeField] private AttackSystem _attackSystem;
		[SerializeField] private float baseDamage;
		[SerializeField] private GameObject _attackAreaPrefab;
		private Collider2D _area;
		private SpriteRenderer _areaSprite;
		public override void OnActivate()
		{
			Attack.Attack attack = new Attack.Attack();
			attack.Attacker = _player;
			attack.damage = baseDamage;
			attack.attackArea = _area;
			attack.SourcePosition = _player.transform.position;
			_player.StartCoroutine(AttackRoutine(attack));
		}

		IEnumerator AttackRoutine(Attack.Attack attack)
		{
			_areaSprite.enabled = true;
			_attackSystem.Attack(attack);
			yield return new WaitForSeconds(0.1f);
			_areaSprite.enabled = false;
		}
		public override void OnGainAbility()
		{
			base.OnGainAbility();
			var areaGO = Instantiate(_attackAreaPrefab, _player.PlayerMovement.FacingTransform);
			_area = areaGO.GetComponent<Collider2D>();
			_areaSprite = areaGO.GetComponent<SpriteRenderer>();
			_areaSprite.enabled = false;
		}

		public override void OnLoseAbility()
		{
			Destroy(_area.gameObject);
		}
	}
}