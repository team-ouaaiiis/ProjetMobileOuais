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


        [HideInInspector] public GameObject soundObject;
        [HideInInspector] public AudioSource audioSource;

        public void PlaySound()
        {
            audioSource.panStereo = 0;
            audioSource.Play();
        }

        public void PlaySoundAt(float _xPos)
        {
            _xPos = Mathf.Clamp(_xPos, -1, 1);
            audioSource.panStereo = _xPos;
            audioSource.Play();
        }

        public void StopSound()
        {
            audioSource.Stop();
        }

    }

    public Sound[] sounds;

    [ReadOnly] public Dictionary<string,Sound> soundDictionary = new Dictionary<string, Sound>();

    public static AudioManager instance;

    #region Monobehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    #endregion

    [Button]
    void CreateSounds()
    {
        if (soundDictionary.Count > 0)
        {
            DeleteAllSounds();
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            //create game object
            GameObject audioGO = new GameObject(sounds[i].name);
            audioGO.transform.SetParent(transform);
            sounds[i].soundObject = audioGO;

            //Create & configure audio source
            AudioSource source = audioGO.AddComponent<AudioSource>();
            source.clip = sounds[i].audioClip;
            source.playOnAwake = sounds[i].playOnAwake;
            source.volume = sounds[i].volume;
            source.pitch = sounds[i].pitch;
            source.priority = sounds[i].priority;
            sounds[i].audioSource = source;

            //Add audio source to list
            soundDictionary.Add(sounds[i].name,sounds[i]);
        }
    }

    [Button]
    void DeleteAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            DestroyImmediate(sounds[i].soundObject);
        }

        soundDictionary = new Dictionary<string, Sound>();
    }

    #region Public Methods

    /// <summary>
    /// Play sound
    /// </summary>
    /// <param name="_soundName">The sname of the sound to play</param>
    public void PlaySound(string _soundName)
    {
        soundDictionary[_soundName].PlaySound();
    }

    public void StopSound(string _soundName)
    {
        soundDictionary[_soundName].StopSound();
    }

    #endregion
}
