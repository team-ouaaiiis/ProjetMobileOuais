using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkPattern))]
public class ChunkPatternEditor : Editor
{
    private ChunkPattern chunkPattern;
    private Object objectSource;
    private Entity entity;

    private void OnEnable()
    {
        chunkPattern = (ChunkPattern)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        for (int i = 0; i < 2; i++)
        {
            Object source = new Object();
            source = EditorGUILayout.ObjectField(source, typeof(Object), true);
            
        }
    }
}
