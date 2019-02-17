using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkPattern))]
[ExecuteInEditMode]
public class ChunkPatternEditor : Editor
{
    [SerializeField] private SerializedObject GetTarget;
    [SerializeField] private ChunkPattern chunkPattern;
    [SerializeField] private SerializedProperty chunkElements;
    private Editor editorPreview;

    private void OnEnable()
    {
        chunkPattern = (ChunkPattern)target;
        GetTarget = new SerializedObject(chunkPattern);
        
        AssignProperties();
    }

    private void OnDisable()
    {
        
        
    }

    private void AssignProperties()
    {
        chunkElements = GetTarget.FindProperty("chunkElements");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GetTarget.Update();
        EditorGUI.BeginChangeCheck();
        DrawRects();
        GetTarget.ApplyModifiedProperties();
    }

    private void DrawRects()
    {
        if(chunkPattern.ChunkElements.Count != ChunkManagerReferencer.instance.ChunkManager.Columns * ChunkManagerReferencer.instance.ChunkManager.Rows)
        {
            chunkPattern.ChangeSize(ChunkManagerReferencer.instance.ChunkManager.Columns * ChunkManagerReferencer.instance.ChunkManager.Rows);
        }

        int z = 0;
        for (int x = 0; x < ChunkManagerReferencer.instance.ChunkManager.Columns; x++)
        {
            for (int y = 0; y < ChunkManagerReferencer.instance.ChunkManager.Rows; y++)
            {
                var objectRect = new Rect(60 + x * 120, 100 + 120 * y, 100, 80);
                if (z < chunkElements.arraySize)
                {
                    SerializedProperty property = chunkElements.GetArrayElementAtIndex(z).FindPropertyRelative("prefab");
                    property.objectReferenceValue = EditorGUI.ObjectField(objectRect, property.objectReferenceValue, typeof(GameObject), false);
                    if (property.objectReferenceValue != null)
                    {
                        //Editor editor = CreateEditor(property.objectReferenceValue);
                        var previewRect = new Rect(60 + x * 120, 100 + 120 * y, 85, 80);
                        OnInteractivePreviewGUI(previewRect, EditorStyles.label);
                    }

                    z++;
                }
                    
            }
        }
    }
}
