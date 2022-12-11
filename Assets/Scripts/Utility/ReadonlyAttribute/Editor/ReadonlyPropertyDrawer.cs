using UnityEditor;
using UnityEngine;


	[CustomPropertyDrawer(typeof(ReadonlyAttribute))]
	public class ReadonlyPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// EditorGUI.BeginChangeCheck();
			
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label);
			GUI.enabled = true;
			// Call OnValueChanged callbacks
			// EditorGUI.EndChangeCheck();

		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label);
		}
	}
