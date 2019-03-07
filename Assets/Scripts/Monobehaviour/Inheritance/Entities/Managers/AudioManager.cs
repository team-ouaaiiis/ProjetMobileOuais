using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AudioManager : Manager
{
    [System.Serializable]
    public class Sound
    {
        public string name = "unknown sound";
        public AudioClip audioClip;
        public bool playOnAwake = false;
        [Range(0.0f, 1.0f)] public float volume = 1.0f;
        [Range(-3.0f, 3.0f)] public float pitch = 1;
        [Range(0, 256)] public int priority = 128;
    }

    public Sound[] sounds;

    [ReadOnly] public List<AudioSource> audioSources = new List<AudioSource>();

    public static AudioManager instance;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    [Button]
    public void CreateSounds()
    {
        if (audioSources.Count > 0)
        {
            DeleteAllSounds();
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            //create game object
            GameObject audioGO = new GameObject(sounds[i].name);
            audioGO.transform.SetParent(transform);

            //Create & configure audio source
            AudioSource source = audioGO.AddComponent<AudioSource>();
            source.clip = sounds[i].audioClip;
            source.playOnAwake = sounds[i].playOnAwake;
            source.volume = sounds[i].volume;
            source.pitch = sounds[i].pitch;
            source.priority = sounds[i].priority;

            //Add audio source to list
            audioSources.Add(source);

        }
    }

    [Button]
    public void DeleteAllSounds()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            DestroyImmediate(audioSources[i].gameObject);
        }

        audioSources = new List<AudioSource>();
    }

    public void PlaySound()
    {

    }
}
