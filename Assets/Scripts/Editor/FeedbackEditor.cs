using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Feedback))]
public class FeedbackEditor : Editor
{
    [SerializeField] private Feedback feedback;
    [SerializeField] private SerializedObject GetTarget;

    [SerializeField] private SerializedProperty feedbackName;

    [Header("Particle System")]
    [SerializeField] private SerializedProperty useParticleSystem;
    [SerializeField] private SerializedProperty ps;

    [Header("Cam Shake")]
    [SerializeField] private SerializedProperty useCamShake;
    [SerializeField] private SerializedProperty shakeDuration;
    [SerializeField] private SerializedProperty shakeAmount;

    [Header("Cam Zoom")]
    [SerializeField] private SerializedProperty useZoom;
    [SerializeField] private SerializedProperty curveZoom;
    [SerializeField] private SerializedProperty speedZoom;
    [SerializeField] private SerializedProperty shouldStay;

    [Header("Freeze Frame")]
    [SerializeField] private SerializedProperty useFreezeFrame;
    [SerializeField] private SerializedProperty curveFreeze;
    [SerializeField] private SerializedProperty speedFreeze;

    [Header("Sound")]
    [SerializeField] private SerializedProperty useSound;
    [SerializeField] private SerializedProperty sound;

    private void OnEnable()
    {
        feedback = (Feedback)target;
        GetTarget = new SerializedObject(feedback);
        AssignProperties();
    }

    private void AssignProperties()
    {
        feedbackName = GetTarget.FindProperty("feedbackName");
        useParticleSystem = GetTarget.FindProperty("playParticleSystem");
        ps = GetTarget.FindProperty("particle");

        useCamShake = GetTarget.FindProperty("playCamShake");
        shakeDuration = GetTarget.FindProperty("shakeDuration");
        shakeAmount = GetTarget.FindProperty("shakeAmount");

        useZoom = GetTarget.FindProperty("playZoom");
        curveZoom = GetTarget.FindProperty("curveZoom");
        speedZoom = GetTarget.FindProperty("speedZoom");
        shouldStay = GetTarget.FindProperty("shouldStay");

        useFreezeFrame = GetTarget.FindProperty("playFreezeFrame");
        curveFreeze = GetTarget.FindProperty("curveFreeze");
        speedFreeze = GetTarget.FindProperty("speedFreeze");

        useSound = GetTarget.FindProperty("playSound");
        sound = GetTarget.FindProperty("sound");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        GetTarget.Update();
        EditorGUILayout.PropertyField(feedbackName);
        CamShakeEditor();
        CamZoomEditor();
        FreezeFrameEditor();
        ParticleSystemEditor();
        SoundEditor();
        GetTarget.ApplyModifiedProperties();
    }

    private void CamShakeEditor()
    {
        EditorGUILayout.PropertyField(useCamShake);

        if (feedback.UseCamShake)
        {
            EditorGUILayout.PropertyField(shakeDuration);
            EditorGUILayout.PropertyField(shakeAmount);
        }
    }

    private void CamZoomEditor()
    {
        EditorGUILayout.PropertyField(useZoom);

        if (feedback.UseZoom)
        {
            EditorGUILayout.PropertyField(curveZoom);
            EditorGUILayout.PropertyField(speedZoom);
        }
    }

    private void FreezeFrameEditor()
    {
        EditorGUILayout.PropertyField(useFreezeFrame);

        if (feedback.UseFreezeFrame)
        {
            EditorGUILayout.PropertyField(curveFreeze);
            EditorGUILayout.PropertyField(speedFreeze);
        }
    }

    private void ParticleSystemEditor()
    {
        EditorGUILayout.PropertyField(useParticleSystem);

        if (feedback.UseParticleSystem)
        {
            EditorGUILayout.PropertyField(ps);
        }
    }

    private void SoundEditor()
    {
        EditorGUILayout.PropertyField(useSound);

        if (feedback.UseSound)
        {
            EditorGUILayout.PropertyField(sound);
        }
    }
}
