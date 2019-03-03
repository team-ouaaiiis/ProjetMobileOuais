using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MaterialPropertySharer))]
[ExecuteInEditMode]
public class MaterialPropertySharerEditor : Editor
{
    [SerializeField] private SerializedObject GetTarget;
    [SerializeField] private MaterialPropertySharer materialPropertySharer;
    [SerializeField] private SerializedProperty matProperties;
    [SerializeField] private int ListSize;

    private void OnEnable()
    {
        materialPropertySharer = (MaterialPropertySharer)target;
        GetTarget = new SerializedObject(materialPropertySharer);
        matProperties = GetTarget.FindProperty("properties");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();

        GetTarget.Update();
        materialPropertySharer.Update();
        ListSizeManager();
        StructFields();       

        GetTarget.ApplyModifiedProperties();
    }

    /// <summary>
    /// Manages List Size
    /// </summary>
    private void ListSizeManager()
    {
        Space(2);
        ListSize = matProperties.arraySize;
        ListSize = EditorGUILayout.IntField("Properties Number :", ListSize);

        if (ListSize != matProperties.arraySize)
        {
            while (ListSize > matProperties.arraySize)
            {
                matProperties.InsertArrayElementAtIndex(matProperties.arraySize);
            }

            while (ListSize < matProperties.arraySize)
            {
                matProperties.DeleteArrayElementAtIndex(matProperties.arraySize - 1);
            }
        }
    }

    private void StructFields()
    {

        Space(2);

        for (int i = 0; i < materialPropertySharer.Properties.Length; i++)
        {
            materialPropertySharer.Properties[i].FoldEvent = EditorGUILayout.Foldout(materialPropertySharer.Properties[i].FoldEvent, materialPropertySharer.Properties[i].Name);

            if (materialPropertySharer.Properties[i].FoldEvent) // FOLDER
            {

                SerializedProperty eventListRef = matProperties.GetArrayElementAtIndex(i);

                SerializedProperty Name = eventListRef.FindPropertyRelative("name");
                SerializedProperty PropertyType = eventListRef.FindPropertyRelative("propertyType");
                SerializedProperty VectorValue = eventListRef.FindPropertyRelative("vectorValue");
                SerializedProperty FloatValue = eventListRef.FindPropertyRelative("floatValue");
                SerializedProperty ColorValue = eventListRef.FindPropertyRelative("colorValue");

                EditorGUILayout.PropertyField(Name);
                EditorGUILayout.PropertyField(PropertyType);

                if (materialPropertySharer.Properties[i].PropertyTypeEnum == global::PropertyType.Color)
                {
                    EditorGUILayout.PropertyField(ColorValue);
                }

                else if (materialPropertySharer.Properties[i].PropertyTypeEnum == global::PropertyType.Float)
                {
                    EditorGUILayout.PropertyField(FloatValue);
                }

                else if (materialPropertySharer.Properties[i].PropertyTypeEnum == global::PropertyType.Vector)
                {
                    EditorGUILayout.PropertyField(VectorValue, true);
                }

                Space(2);

            }
        }
    }

    /// <summary>
    /// Spacing
    /// </summary>
    /// <param name="count"></param>
    private void Space(int count)
    {
        for (int i = 0; i < count; i++)
        {
            EditorGUILayout.Space();
        }
    }
}
