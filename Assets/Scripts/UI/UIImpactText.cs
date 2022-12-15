using System;
using System.Collections;
using RogueDescent.Attack;
using RogueDescent.Pooling;
using TMPro;
using UnityEngine;

namespace RogueDescent.UI
{
	public class UIImpactText : PooledObject
	{
		private Impact _impact;
		private TMP_Text _text;
		private float _delay = 0.5f;
		private float _timer = 0;
		private void Awake()
		{
			_text = GetComponent<TMP_Text>();
		}

		//Coroutines just ... feel too expensive for this.
		private void Update()
		{
			_timer += Time.deltaTime;
			transform.localPosition = transform.localPosition + Vector3.up*Time.deltaTime/5f;
			if (_timer >= _delay)
			{
				ReturnSelfToPool();
			}
		}
		

		public void SetImpact(Impact impact)
		{
			//if impact/coroutine is already running, cancel it and restart.
			_impact = impact;
			var rectPos = UIUtility.WorldToRectLocalPosition(Camera.main, transform.parent as RectTransform, impact.ImpactLocation);
			transform.localPosition = rectPos;
			_text.text = _impact.RealDamage.ToString("N");
			_timer = 0;
		}
	}
}