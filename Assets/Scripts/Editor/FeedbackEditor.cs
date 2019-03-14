using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Feedback))]
public class FeedbackEditor : Editor
{
    #region Fields

    [SerializeField] private Feedback feedback;
    [SerializeField] private SerializedObject GetTarget;

    [SerializeField] private SerializedProperty feedbackName;
    [SerializeField] private SerializedProperty feedbackKey;

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

    [Header("Blink")]
    [SerializeField] private SerializedProperty useBlink;
    [SerializeField] private SerializedProperty blinkMat;
    [SerializeField] private SerializedProperty blinkCount;
    [SerializeField] private SerializedProperty blinkDelay;
    [SerializeField] private SerializedProperty blinkTime;
    [SerializeField] private SerializedProperty blinkColor;

    [Header("Object Shake")]
    [SerializeField] private SerializedProperty playObjectShake;
    [SerializeField] private SerializedProperty objectToShake;
    [SerializeField] private SerializedProperty curveObjectToShake;
    [SerializeField] private SerializedProperty intensityShake;
    [SerializeField] private SerializedProperty speedShake;
    [SerializeField] private SerializedProperty spaceShake;
    [SerializeField] private SerializedProperty axesShake;

    [Header("Animator")]
    [SerializeField] private SerializedProperty playAnim;
    [SerializeField] private SerializedProperty anim;
    [SerializeField] private SerializedProperty trigger;

    #endregion

    private void OnEnable()
    {
        feedback = (Feedback)target;
        GetTarget = new SerializedObject(feedback);
        AssignProperties();
    }

    private void AssignProperties()
    {
        feedbackName = GetTarget.FindProperty("feedbackName");
        feedbackKey = GetTarget.FindProperty("debugKey");

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

        useBlink = GetTarget.FindProperty("useBlink");
        blinkCount = GetTarget.FindProperty("blinkCount");
        blinkMat = GetTarget.FindProperty("blinkMat");
        blinkTime = GetTarget.FindProperty("blinkTime");
        blinkDelay = GetTarget.FindProperty("blinkDelay");
        blinkColor = GetTarget.FindProperty("blinkCol");

        playObjectShake = GetTarget.FindProperty("playObjectShake");
        objectToShake = GetTarget.FindProperty("objectToShake");
        curveObjectToShake = GetTarget.FindProperty("curveObjectToShake");
        intensityShake = GetTarget.FindProperty("intensityShake");
        speedShake = GetTarget.FindProperty("speedShake");
        spaceShake = GetTarget.FindProperty("spaceShake");
        axesShake = GetTarget.FindProperty("shakeAxes");

        playAnim = GetTarget.FindProperty("playAnim");
        anim = GetTarget.FindProperty("animator");
        trigger = GetTarget.FindProperty("trigger");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        GetTarget.Update();
        EditorGUILayout.PropertyField(feedbackName);
        EditorGUILayout.PropertyField(feedbackKey);
        CamShakeEditor();
        CamZoomEditor();
        FreezeFrameEditor();
        AnimationEditor();
        ParticleSystemEditor();
        SoundEditor();
        BlinkEditor();
        ObjectShakeEditor();
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

    private void BlinkEditor()
    {
        EditorGUILayout.PropertyField(useBlink);

        if (feedback.UseBlink)
        {
            EditorGUILayout.PropertyField(blinkMat);
            EditorGUILayout.PropertyField(blinkColor);
            EditorGUILayout.PropertyField(blinkCount);
            EditorGUILayout.PropertyField(blinkDelay);
            EditorGUILayout.PropertyField(blinkTime);
        }
    }

    private void ObjectShakeEditor()
    {
        EditorGUILayout.PropertyField(playObjectShake);

        if (feedback.PlayObjectShake)
        {
            EditorGUILayout.PropertyField(objectToShake);
            EditorGUILayout.PropertyField(spaceShake);
            EditorGUILayout.PropertyField(curveObjectToShake);
            EditorGUILayout.PropertyField(speedShake);
            EditorGUILayout.PropertyField(intensityShake);
            EditorGUILayout.PropertyField(axesShake);
        }
    }

    private void AnimationEditor()
    {
        EditorGUILayout.PropertyField(playAnim);

        if (feedback.PlayAnim)
        {
            EditorGUILayout.PropertyField(anim);
            EditorGUILayout.PropertyField(trigger);
        }
    }
}
