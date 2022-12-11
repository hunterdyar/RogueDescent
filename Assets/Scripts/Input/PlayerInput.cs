using System;
using UnityEngine;

namespace RogueDescent.Input
{
	public class PlayerInput : MonoBehaviour
	{
		private Player _player;

		private void Awake()
		{
			_player = GetComponent<Player>();
		}

		private void Update()
		{
			_player.PlayerMovement.Move(new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical")));
			
			if (UnityEngine.Input.GetButtonDown("Jump"))
			{
				_player.TryActivateAbility(0);
			}
		}
	}
}