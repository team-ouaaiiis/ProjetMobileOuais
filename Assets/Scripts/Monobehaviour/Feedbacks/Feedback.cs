using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Feedback : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private string feedbackName = "";
    [SerializeField] private KeyCode debugKey = KeyCode.G;

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

    [Header("Blink")]
    [SerializeField] private bool useBlink = false;
    [SerializeField] private Renderer blinkMat;
    [SerializeField] private Color blinkCol;
    [SerializeField] private int blinkCount = 3;
    [SerializeField] private float blinkDelay = 0.01f;
    [SerializeField] private float blinkTime = 0.02f;

    [Header("Object Shake")]
    [SerializeField] private bool playObjectShake = false;
    [SerializeField] private GameObject objectToShake;
    [SerializeField] private AnimationCurve curveObjectToShake = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1), new Keyframe(1, 0) });
    [SerializeField] private float intensityShake = 0.08f;
    [SerializeField] private float speedShake = 2f;
    [SerializeField] private Space spaceShake = Space.Local;
    [SerializeField] private Vector3 shakeAxes = new Vector3(1, 1, 1);

    [Header("Animation")]
    [SerializeField] private bool playAnim = false;
    [SerializeField] private Animator animator;
    [SerializeField] private string trigger = "In";

    private void Update()
    {
        if(Input.GetKeyDown(debugKey))
        {
            PlayFeedback();
        }
        
    }

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

        if(UseFreezeFrame)
        {
            FeedbackManager.instance.CallFreezeFrame(curveFreeze, speedFreeze);
        }

        if(UseBlink)
        {
            FeedbackManager.instance.CallBlink(blinkCol, blinkMat, blinkCount, blinkDelay, blinkTime);
        }

        if(playObjectShake)
        {
            FeedbackManager.instance.CallShakeObject(objectToShake, curveObjectToShake, intensityShake, speedShake, spaceShake, shakeAxes);
        }

        if(PlayAnim)
        {
            animator.SetTrigger(trigger);
        }
    }

    #region Properties

    public bool UseParticleSystem { get => playParticleSystem; set => playParticleSystem = value; }
    public bool UseCamShake { get => playCamShake; set => playCamShake = value; }
    public bool UseZoom { get => playZoom; set => playZoom = value; }
    public bool UseFreezeFrame { get => playFreezeFrame; set => playFreezeFrame = value; }
    public bool UseSound { get => playSound; set => playSound = value; }
    public string FeedbackName { get => feedbackName; set => feedbackName = value; }
    public bool UseBlink { get => useBlink; set => useBlink = value; }
    public bool PlayObjectShake { get => playObjectShake; set => playObjectShake = value; }
    public bool PlayAnim { get => playAnim; set => playAnim = value; }

    #endregion
}
