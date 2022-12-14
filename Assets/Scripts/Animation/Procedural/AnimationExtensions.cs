using UnityEngine;

namespace RogueDescent.Animation
{
	public static class AnimationExtensions
	{
		/// <summary>
		/// Starts the flash coroutine in the SRProcAnim class.
		/// </summary>
		/// <param name="spriteRenderer">Sprite to flash</param>
		/// <param name="context">The object to call 'StartCoroutine' on. Probably should be "this".</param>
		/// <param name="color">Change the sprite renderer color to this.</param>
		/// <param name="duration">Duration of flash. Use 0 for a single frame.</param>
		public static void Flash(this SpriteRenderer spriteRenderer, MonoBehaviour context, Color color, float duration = 0)
		{
			context.StartCoroutine(SRProcAnim.FlashShader(spriteRenderer,color, duration));
		}

		/// <summary>
		/// Flash a sprite multiple times.
		/// </summary>
		/// <param name="spriteRenderer">Sprite to flash</param>
		/// <param name="context">The object to call 'StartCoroutine' on. Probably should be "this".</param>
		/// <param name="color">The color will turn to this, using a shader with "_IgnoreTexture" and "_Color" properties, and ignoring the sprite renderer multiply color (set to white during flash)</param>
		/// <param name="onDuration">Time the sprite will be this color for each flash</param>
		/// <param name="offDuration">Time in between flashes.</param>
		/// <param name="cycles">Number of times to flash.</param>
		public static void FlashRepeated(this SpriteRenderer spriteRenderer, MonoBehaviour context, Color color, float onDuration,float offDuration,int cycles)
		{
			context.StartCoroutine(SRProcAnim.FlashShaderRepeated(context,spriteRenderer, color, onDuration,offDuration,cycles));
		}
	}
}