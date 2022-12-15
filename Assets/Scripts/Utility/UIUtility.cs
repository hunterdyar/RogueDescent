using UnityEngine;

namespace RogueDescent
{
	public static class UIUtility
	{
		public static Vector2 WorldToRectLocalPosition(Camera camera,RectTransform rect, Vector3 worldPosition)
		{
			Vector2 screenPoint = camera.WorldToScreenPoint(worldPosition);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, null, out var rectPos);
			return rectPos;
		}
	}
}