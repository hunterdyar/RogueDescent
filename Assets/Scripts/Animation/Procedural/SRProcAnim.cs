using System.Collections;
using UnityEngine;

namespace RogueDescent.Animation
{
	/// <summary>
	/// Procedural Animations for Sprite Rendererers
	/// </summary>
	public static class SRProcAnim
	{
		public static readonly int ColorProp = Shader.PropertyToID("_Color");
		public static readonly int IgnoreTexture = Shader.PropertyToID("_IgnoreTexture");

		/// <param name="spriteRenderer">SpriteRenderer to flash. Only adjusts the Sprite Renderer "Color" Property.</param>
		/// <param name="color">Color to flash.</param>
		/// <param name="duration">Use 0 for a single frame, ignorant of framerate. Recommended to hard-set this to a value.</param>
		public static IEnumerator Flash(SpriteRenderer spriteRenderer,Color color, float duration)
		{
			//a proper flash will probably modify a shader. We will get to that soon using ShaderGraph!
			Color defaultColor = spriteRenderer.color;
			spriteRenderer.color = color;
			if (duration > 0)
			{
				yield return new WaitForSeconds(duration);
			}
			else
			{
				yield return new WaitForEndOfFrame();
			}

			spriteRenderer.color = defaultColor;
		}

		public static IEnumerator FlashShader(SpriteRenderer spriteRenderer, Color color, float duration)
		{
			//a proper flash will probably modify a shader. We will get to that soon using ShaderGraph!
			//cache material
			Material mat = spriteRenderer.material;
			//store default references to return to after flashing
			var srColor = spriteRenderer.color;
			Color defaultColor = mat.GetColor(ColorProp);
			
			//adjust colors
			//un-adjust any character adjustments to colors so the sprite color is pure.
			spriteRenderer.color = Color.white;
			mat.SetColor(ColorProp,color);
			mat.SetFloat(IgnoreTexture, 1);
			
			//delay. The flash.
			if (duration > 0)
			{
				yield return new WaitForSeconds(duration);
			}
			else
			{
				yield return new WaitForEndOfFrame();
			}

			//reset colors
			spriteRenderer.color = srColor;
			mat.SetFloat(IgnoreTexture, 0);
			mat.SetColor(ColorProp, defaultColor);
		}

		public static IEnumerator FlashShaderRepeated(MonoBehaviour context, SpriteRenderer spriteRenderer, Color color, float flashOnDuration, float flashOffDuration, int flashCycles)
		{
			for (int i = 0; i < flashCycles; i++)
			{
				yield return context.StartCoroutine(FlashShader(spriteRenderer, color, flashOnDuration));
				if (i < flashCycles - 1)
				{
					yield return new WaitForSeconds(flashOffDuration);
				}
			}
		}
	}
}