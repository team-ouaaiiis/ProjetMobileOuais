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
    [SerializeField] Editor gameObjectEditor;
    //[SerializeField] Rect preview;

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
        GetTarget.ApplyModifiedProperties();
    }

    //public override void OnPreviewGUI(Rect r, GUIStyle background)
    //{
    //        if (gameObjectEditor == null)
    //            gameObjectEditor = CreateEditor(chunkPattern.ChunkElements[0].Entity.gameObject);
    //        gameObjectEditor.OnPreviewGUI(r, background);
    //}

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
                    SerializedProperty property = chunkElements.GetArrayElementAtIndex(z).FindPropertyRelative("entity");

                    property.objectReferenceValue = EditorGUI.ObjectField(objectRect, property.objectReferenceValue, typeof(GameObject), false);

                    chunkPattern.ChunkElements[z].XPos = x;
                    chunkPattern.ChunkElements[z].ZPos = y;
                    
                    if (property.objectReferenceValue != null)
                    {
                        //gameObjectEditor = CreateEditor(chunkPattern.ChunkElements[z].Entity);
                        //Rect preview = new Rect(60 + x * 120, 100 + 120 * y, 85, 80);
                        //OnPreviewGUI(preview, EditorStyles.whiteLabel);
                        //Texture2D tex = AssetPreview.GetAssetPreview(property.objectReferenceValue);
                        //if(tex != null)
                        //EditorGUI.DrawPreviewTexture(preview, tex);
                        //DestroyImmediate(gameObjectEditor);
                    }                   

                    z++;
                }
                    
            }
        }
    }
}
