using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Shake
{
    #region Fields

    [Header("Test")]
    [SerializeField] private KeyCode debugBind = KeyCode.N;
    [SerializeField] private bool enableDebug = false;
    [SerializeField] private float amountTest = 0.7f;
    [SerializeField] private float durationTest = 10f;

    private Vector3 initialPos;
    private float initalAmount;
    private float initalDuration;
    private bool firstCoroutineCall = true;

    #endregion

    #region MonoBehaviour Callbacks

    private void Update()
    {
        if (Input.GetKeyDown(debugBind) && enableDebug)
        {
            Shake(durationTest, amountTest);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Test the shake.
    /// </summary>
    public void ShakeTest()
    {
        Shake(durationTest, amountTest);
    }

    /// <summary>
    /// Shake the object with a given amount and duration.
    /// </summary>
    /// <param name="duration"> The duration of the shake. </param>
    /// <param name="amount"> The intensity of the shake. </param>
    public void Shake(float duration, float amount)
    {
        switch(Space)
        {
            case Space.Local:
                initialPos = ObjectToShake.transform.localPosition;
                break;

            case Space.World:
                initialPos = ObjectToShake.transform.position;
                break;
        }

        firstCoroutineCall = true;
        StartCoroutine(Shaking(duration, amount));
    }

    public IEnumerator Shaking(float duration, float amount)
    {
        if (firstCoroutineCall)
        {
            initalAmount = amount;
            initalDuration = duration;
            firstCoroutineCall = false;
        }

        if (duration > 0.0f)
        {
            float xShake = initialPos.x + (Random.insideUnitSphere.x * amount * BoolToInt(X));
            float yShake = initialPos.y + (Random.insideUnitSphere.y * amount * BoolToInt(Y));
            float zShake = initialPos.z + (Random.insideUnitSphere.z * amount * BoolToInt(Z));

            switch (Space)
            {
                case Space.Local:
                    ObjectToShake.transform.localPosition = new Vector3(xShake, yShake, zShake);
                    break;

                case Space.World:
                    ObjectToShake.transform.position = new Vector3(xShake, yShake, zShake);
                    break;
            }

            duration--;
            amount = Mathf.Lerp(initalAmount, 0, Mathf.InverseLerp(initalDuration, 0, duration));
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(Shaking(duration, amount));
        }

        else
        {
            amount = 0;
            duration = 0;

            switch (Space)
            {
                case Space.Local:
                    ObjectToShake.transform.localPosition = initialPos;
                    break;

                case Space.World:
                    ObjectToShake.transform.position = initialPos;
                    break;
            }
        }
    }

    #endregion
}

