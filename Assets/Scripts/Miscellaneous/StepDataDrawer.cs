#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StepData))]
public class StepDataDrawer : PropertyDrawer
{
    // Helper class to add a bit of spacing between elements of a serialized list of StepData

    float margin = 5f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true) + margin;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height -= margin;
        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif
