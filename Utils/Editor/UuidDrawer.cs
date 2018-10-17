using UnityEditor;
using UnityEngine;

namespace Blatand.Utils
{
    /// <seealso cref="Uuid"/>  
    [CustomPropertyDrawer(typeof(Uuid))]
    public class UuidDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("m_uuid"), label);
        }
    }
}

