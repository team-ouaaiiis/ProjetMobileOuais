using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [ExecuteInEditMode]
    public class Sound : MonoBehaviour
    {
        [SerializeField] private int objectToInstantiate = 5;
        [SerializeField] private AudioClip[] clips;
        [HideInInspector] [SerializeField] private List<AudioSource> sources = new List<AudioSource>();
        [SerializeField] [Range(0,1)] private float randomPitch = 0f;

        private void Update()
        {
            if(clips.Length > 0 )
            {
                if(clips[0] != null)
                {
                    gameObject.name = clips[0].name;
                }
            }
        }

        /// <summary>
        /// Plays the associated sound.
        /// </summary>
        /// <param name="pitchRandom"></param>
        public virtual void PlayThisSound()
        {
            AudioSource sourceToPlay = NewAudioSource();
            sourceToPlay.clip = clips[Random.Range(0, clips.Length - 1)];
            sourceToPlay.pitch = 1 + Random.Range(-randomPitch, randomPitch);
            sourceToPlay.Play();
            //sourceToPlay.gameObject.SetActive(false);
        }

        public virtual void PlayThisSound(float xPos)
        {
            AudioSource sourceToPlay = NewAudioSource();
            sourceToPlay.clip = clips[Random.Range(0, clips.Length - 1)];
            sourceToPlay.pitch = 1 + Random.Range(-randomPitch, randomPitch);
            sourceToPlay.panStereo = CustomMethod.Interpolate(-0.8f, 0.8f, -50, 50f, xPos);
            sourceToPlay.Play();
            //sourceToPlay.gameObject.SetActive(false);
        }

        public void StopThisSound()
        {
            for (int i = 0; i < sources.Count; i++)
            {
                sources[i].Stop();
            }
        }

        /// <summary>
        /// Pooling
        /// </summary>
        /// <returns></returns>
        private AudioSource NewAudioSource()
        {
            for (int i = 0; i < Sources.Count; i++)
            {
                if(!Sources[i].isPlaying)
                {
                    return Sources[i];
                }
            }

            return null;
        }

        [ContextMenu("Instantiate all sources.")]
        private void InstantiateSources()
        {
            DestroyAllSources();

            for (int i = 0; i < objectToInstantiate; i++)
            {
                GameObject newObject = new GameObject();
                newObject.transform.parent = transform;
                newObject.AddComponent<AudioSource>();
                newObject.name = gameObject.name + "_Source_0" + i;
                AudioSource newSource = newObject.GetComponent<AudioSource>();
                newSource.clip = clips[0];
                newSource.playOnAwake = false;
                newSource.loop = false;
                Sources.Add(newSource);
            }
            
            if(!SoundManager.instance.Sounds.Contains(this))
            {
                SoundManager.instance.Sounds.Add(this);
            }
        }
        
        [ContextMenu("Destroy all Sources")]
        private void DestroyAllSources()
        {
            if(Sources.Count > 0)
            {
                for (int i = 0; i < Sources.Count; i++)
                {
                    DestroyImmediate(Sources[i].gameObject);
                }

                sources.Clear();
            }
        }

        #region Properties

        public List<AudioSource> Sources
        {
            get
            {
                return sources;
            }

            set
            {
                sources = value;
            }
        }

        #endregion
    }

