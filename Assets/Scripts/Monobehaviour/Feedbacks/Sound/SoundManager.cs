using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<Sound> sounds = new List<Sound>();
        [SerializeField] private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
        public static SoundManager instance;

        public List<Sound> Sounds
        {
            get
            {
                return sounds;
            }

            set
            {
                sounds = value;
            }
        }

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {    
            for (int i = 0; i < Sounds.Count; i++)
            {
                soundDictionary.Add(Sounds[i].gameObject.name, Sounds[i]);
            }
        }

        [ContextMenu("Initialize")]
        public void Initialize()
        {
            instance = this;
        }

        public virtual void PlayNewSound(string sound)
        {            
            soundDictionary[sound].PlayThisSound();
        }

        public virtual void PlayNewSound(string sound, float xPos)
        {
            soundDictionary[sound].PlayThisSound(xPos);
        }

        public void StopSound(string sound)
        {
            soundDictionary[sound].StopThisSound();
        }
    }

