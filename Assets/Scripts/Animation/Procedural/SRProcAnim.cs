using System.Collections;
using UnityEngine;

namespace RogueDescent.Animation
{
	/// <summary>
	/// Procedural Animations for Sprite Rendererers
	/// </summary>
	public static class SRProcAnim
	{
		private static readonly int ColorProp = Shader.PropertyToID("Color");
		private static readonly int IgnoreTexture = Shader.PropertyToID("IgnoreTexture");

		/// <param name="spriteRenderer">SpriteRenderer to flash.</param>
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
			Material mat = spriteRenderer.material;
			Color defaultColor = mat.GetColor(ColorProp);
			mat.SetColor(ColorProp,color);
			mat.SetFloat(IgnoreTexture, 1);
			
			if (duration > 0)
			{
				yield return new WaitForSeconds(duration);
			}
			else
			{
				yield return new WaitForEndOfFrame();
			}

			mat.SetFloat(IgnoreTexture, 0);
			mat.SetColor(ColorProp, defaultColor);
		}
	}
}