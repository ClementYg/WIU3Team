#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Gives [SerializeReference] CutsceneStep elements a type-picker dropdown
/// and draws their fields inline, instead of the default blank/reorder-only row.
/// Must live in an "Editor" folder (or an editor-only assembly).
/// </summary>
[CustomPropertyDrawer(typeof(CutsceneStep), true)]
public class CutsceneStepDrawer : PropertyDrawer
{
    private static Type[] _stepTypes;

    private static Type[] StepTypes =>
        _stepTypes ??= TypeCache.GetTypesDerivedFrom<CutsceneStep>()
            .Where(t => !t.IsAbstract)
            .OrderBy(t => t.Name)
            .ToArray();

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
        {
            foreach (var child in EnumerateDirectChildren(property))
                height += EditorGUI.GetPropertyHeight(child, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        string currentTypeName = GetShortTypeName(property.managedReferenceFullTypename);
        string buttonLabel = string.IsNullOrEmpty(currentTypeName) ? "Select Step Type..." : currentTypeName;

        if (EditorGUI.DropdownButton(typeRect, new GUIContent(buttonLabel), FocusType.Keyboard))
        {
            var menu = new GenericMenu();
            foreach (var type in StepTypes)
            {
                var capturedType = type;
                menu.AddItem(new GUIContent(type.Name), type.Name == currentTypeName, () =>
                {
                    property.managedReferenceValue = Activator.CreateInstance(capturedType);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }
            menu.DropDown(typeRect);
        }

        float y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (!string.IsNullOrEmpty(property.managedReferenceFullTypename))
        {
            EditorGUI.indentLevel++;
            foreach (var child in EnumerateDirectChildren(property))
            {
                float h = EditorGUI.GetPropertyHeight(child, true);
                EditorGUI.PropertyField(new Rect(position.x, y, position.width, h), child, true);
                y += h + EditorGUIUtility.standardVerticalSpacing;
            }
            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    /// <summary>Yields only the direct child fields of this managed reference (not grandchildren).</summary>
    private static System.Collections.Generic.IEnumerable<SerializedProperty> EnumerateDirectChildren(SerializedProperty property)
    {
        var iterator = property.Copy();
        var end = iterator.GetEndProperty();
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
        {
            yield return iterator.Copy();
            enterChildren = false;
        }
    }

    private static string GetShortTypeName(string managedReferenceFullTypename)
    {
        if (string.IsNullOrEmpty(managedReferenceFullTypename)) return null;
        // Format is "AssemblyName Namespace.TypeName"
        int spaceIdx = managedReferenceFullTypename.LastIndexOf(' ');
        string typeName = spaceIdx >= 0 ? managedReferenceFullTypename[(spaceIdx + 1)..] : managedReferenceFullTypename;
        int dotIdx = typeName.LastIndexOf('.');
        return dotIdx >= 0 ? typeName[(dotIdx + 1)..] : typeName;
    }
}
#endif
