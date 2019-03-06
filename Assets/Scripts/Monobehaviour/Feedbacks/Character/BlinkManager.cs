using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkManager : Entity
{    
    /// <summary>
    /// Makes the material blink.
    /// </summary>
    /// <param name="number">The number of blinks.</param>
    /// <param name="delay">The delay between each blink.</param>
    /// <param name="time">The time of each blink.</param>
    public override void Blink(Color col, Material mat, int count, float delay, float time)
    {
        base.Blink(col, mat, count, delay, time);
        StartCoroutine(Blinking(col, mat, count, delay, time));
    }

    private IEnumerator Blinking(Color col, Material mat, int number, float delay, float time)
    {
        for (int i = 0; i < number; i++)
        {
            mat.SetColor("_BlinkCol", col);
            yield return new WaitForSecondsRealtime(time);
            mat.SetColor("_BlinkCol", Color.black);
            yield return new WaitForSecondsRealtime(delay);
        }
    }
}
