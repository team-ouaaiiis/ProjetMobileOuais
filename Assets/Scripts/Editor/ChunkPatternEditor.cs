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

    private void OnEnable()
    {
        chunkPattern = (ChunkPattern)target;
        GetTarget = new SerializedObject(chunkPattern);
        AssignProperties();
    }

    private void AssignProperties()
    {
        chunkElements = GetTarget.FindProperty("chunkElements");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        GetTarget.Update();
        DrawRects();
        Space(105);
        chunkPattern.CheckIfSave();
        if (!chunkPattern.IsSaved)
        {
            if (GUILayout.Button("Save"))
            {
                chunkPattern.Save();
            }
        }

        else
        {
            EditorGUILayout.LabelField("The Chunk Pattern is Saved.");            
        }

        Space(2);
        if (GUILayout.Button("Clear All"))
        {
            chunkElements.ClearArray();
        }

        GetTarget.ApplyModifiedProperties();
    }

    private void DrawRects()
    {
        if (chunkPattern.ChunkElements.Count != ChunkManagerReferencer.instance.ChunkManager.Columns * ChunkManagerReferencer.instance.ChunkManager.Rows)
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
                    SerializedProperty property = chunkElements.GetArrayElementAtIndex(z).FindPropertyRelative("go");   //Accessing the GameObject property of the chunk Elements
                    property.objectReferenceValue = EditorGUI.ObjectField(objectRect, property.objectReferenceValue, typeof(GameObject), false);  //Creating an object field to assign it in the inspector
                    GameObject GO = (GameObject)property.objectReferenceValue;  //Casting the property as a GameObject.
                    chunkPattern.ChunkElements[z].XPos = x;
                    chunkPattern.ChunkElements[z].ZPos = y;

                    if (GO != null)
                    {
                        //Drawing the Preview of the chosen GameObject
                        GUIStyle bgColor = new GUIStyle();
                        bgColor.normal.background = EditorGUIUtility.whiteTexture;
                        Editor newGameObjectEditor = CreateEditor(GO);
                        Rect preview = new Rect(60 + x * 120, 100 + 120 * y, 85, 80);
                        newGameObjectEditor.OnPreviewGUI(preview, bgColor);

                        //Dont forget to Destroy the new Editor to avoid leaks
                        DestroyImmediate(newGameObjectEditor);
                    }

                    z++;
                }

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
