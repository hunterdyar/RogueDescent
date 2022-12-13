using UnityEngine;

namespace RogueDescent.Animation
{
	public static class AnimationExtensions
	{
		/// <summary>
		/// Starts the flash coroutine in the SRProcAnim class.
		/// </summary>
		/// <param name="spriteRenderer"></param>
		/// <param name="context">The object to call 'StartCoroutine' on. Probably should be "this".</param>
		/// <param name="color">Change the sprite renderer color to this.</param>
		/// <param name="duration">Duration of flash. Use 0 for a single frame.</param>
		public static void Flash(this SpriteRenderer spriteRenderer, MonoBehaviour context, Color color, float duration = 0)
		{
			context.StartCoroutine(SRProcAnim.Flash(spriteRenderer,color, duration));
		}
	}
}