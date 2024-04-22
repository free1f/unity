using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Environment
{
    [CreateAssetMenu(fileName = "TimeSettings", menuName = "freelf/Environment/TimeSettings")]
    public class TimeSettings : ScriptableObject
    {
        public float timeScale = 1000f;
        [Range(0f, 23f)]
        public float initialTime = 12f;
        [Range(0f, 23f)]
        public float sunriseTime = 5f;
        [Range(0f, 23f)]
        public float sunsetTime = 18f;
    }
}
