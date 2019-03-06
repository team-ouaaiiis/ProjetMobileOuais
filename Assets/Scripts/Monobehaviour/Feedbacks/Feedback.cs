﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private string feedbackName = "";

    [Header("Particle System")]
    [SerializeField] private bool playParticleSystem = false;
    [SerializeField] private ParticleSystem particle;

    [Header("Cam Shake")]
    [SerializeField] private bool playCamShake = false;
    [SerializeField] private float shakeDuration = 10f;
    [SerializeField] private float shakeAmount = 0.7f;

    [Header("Cam Zoom")]
    [SerializeField] private bool playZoom = false;
    [SerializeField] private AnimationCurve curveZoom = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1), new Keyframe(1, 1) });
    [SerializeField] private float speedZoom = 10f;
    [SerializeField] private bool shouldStay = false;

    [Header("Freeze Frame")]
    [SerializeField] private bool playFreezeFrame = false;
    [SerializeField] private AnimationCurve curveFreeze = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1), new Keyframe(1, 1) });
    [SerializeField] private float speedFreeze = 5f;

    [Header("Sound")]
    [SerializeField] private bool playSound = false;
    [SerializeField] private AudioClip sound;

    public void PlayFeedback()
    {
        if(UseCamShake)
        {
            FeedbackManager.instance.CallShake(shakeAmount, shakeDuration);
        }

        if(UseParticleSystem)
        {
            particle.Play();
        }

        if(UseSound)
        {

        }

        if (UseZoom)
        {
            FeedbackManager.instance.CallZoom(curveZoom, speedZoom, shouldStay);
        }
    }

    #region Properties

    public bool UseParticleSystem { get => playParticleSystem; set => playParticleSystem = value; }
    public bool UseCamShake { get => playCamShake; set => playCamShake = value; }
    public bool UseZoom { get => playZoom; set => playZoom = value; }
    public bool UseFreezeFrame { get => playFreezeFrame; set => playFreezeFrame = value; }
    public bool UseSound { get => playSound; set => playSound = value; }
    public string FeedbackName { get => feedbackName; set => feedbackName = value; }

    #endregion
}
