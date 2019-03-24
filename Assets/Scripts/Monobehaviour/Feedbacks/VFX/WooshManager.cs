using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class Woosh
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float frameRate = 60f;
    [SerializeField] private List<Vector3> positions = new List<Vector3>();

    public List<Vector3> Positions { get => positions; set => positions = value; }
    public float FrameRate { get => frameRate; set => frameRate = value; }
}

[ExecuteInEditMode]
public class WooshManager : MonoBehaviour
{
    [SerializeField] private List<Woosh> wooshes = new List<Woosh>(1);
    [SerializeField] private LineRenderer line;
    private Woosh lastWoosh;
    private bool isRecording = false;
    private bool isPlaying = false;
    private float playCompletion = 0f;

    private void Update()
    {
        RecordPositions();
        PlayingWoosh();
        if(wooshes.Count > 0)
            lastWoosh = wooshes[wooshes.Count - 1];
    }

    [Button]
    public void PlayWoosh()
    {
        isPlaying = true;
        playCompletion = 0f;        
    }

    private void PlayingWoosh()
    {
        if (!isPlaying) return;

        if(playCompletion < 1f)
        {
            line.positionCount = (int) Mathf.Lerp(0, lastWoosh.Positions.Count, playCompletion);
            line.SetPositions(lastWoosh.Positions.ToArray());
            playCompletion += Time.deltaTime * lastWoosh.FrameRate;
        }

        if(playCompletion >= 1f)
        {
            line.positionCount = (int)Mathf.Lerp(0, lastWoosh.Positions.Count, 1f);
            line.SetPositions(lastWoosh.Positions.ToArray());
            isPlaying = false;
        }

    }

    private void RecordPositions()
    {
        if (!isRecording) return;
        if(lastWoosh.Positions.Count == 0)
        {
            lastWoosh.Positions.Add(transform.position);
        }

        else
        {
            if (transform.position != lastWoosh.Positions[lastWoosh.Positions.Count - 1])
            {
                lastWoosh.Positions.Add(transform.position);
            }
        }       
    }

    [ContextMenu("Record")]
    [Button]
    private void Record()
    {
        lastWoosh.Positions.Clear();
        Debug.Log("Started Recording Woosh.");
        isRecording = true;
    }

    [ContextMenu("Stop")]
    [Button]
    private void StopRecord()
    {
        Debug.Log("Stopped Recording Woosh.");
        isRecording = false;
    }

    [Button]
    private void DestroyLastWoosh()
    {
        wooshes.Remove(lastWoosh);
    }
}
