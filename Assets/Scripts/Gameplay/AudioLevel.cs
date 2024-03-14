using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freelf.Audio;

namespace Freelf.Gameplay
{
    public class AudioLevel : MonoBehaviour
    {
        public string levelTrack;

        private void Start()
        {
            AudioHandler.instance.Play(levelTrack, false);
        }
    }
}