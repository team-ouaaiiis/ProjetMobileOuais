using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRP08.Cafolo
{
    public class ObjectShaker : Shake
    {
        #region Fields

        //[SerializeField] private GameObject objectToShake;
        [Header("Shake Settings")]
        [SerializeField] private float intensity = 0f;

        private Vector3 initialPos;

        public float Intensity
        {
            get
            {
                return intensity;
            }

            set
            {
                intensity = value;
            }
        }

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            ContinuousShake();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes position
        /// </summary>
        private void Initialize()
        {
            switch (Space)
            {
                case Space.Local:
                    initialPos = ObjectToShake.transform.localPosition;
                    break;
                case Space.World:
                    initialPos = ObjectToShake.transform.position;
                    break;
            }
        }

        /// <summary>
        /// Continuously shakes object based on Amount parameter.
        /// </summary>
        private void ContinuousShake()
        {
            switch (Space)
            {
                case Space.Local:
                    ObjectToShake.transform.localPosition = initialPos + RandomInsideUnit() * Intensity * Time.deltaTime;
                    break;
                case Space.World:
                    ObjectToShake.transform.position = initialPos + RandomInsideUnit() * Intensity * Time.deltaTime;
                    break;
            }
        }        

        /// <summary>
        /// Returns either a Random.insideUnitCircle or Random.insideUnitSphere based on a dimension parameter.
        /// </summary>
        /// <param name="isThreeDimensional"></param>
        /// <returns></returns>
        private Vector3 RandomInsideUnit()
        {
            Vector3 sphere = Random.insideUnitSphere;
            return new Vector3(sphere.x * BoolToInt(X), sphere.y * BoolToInt(Y), sphere.z * BoolToInt(Z));
        }

        #endregion
    }
}

