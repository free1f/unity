using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Environment
{
    public class Campfire : MonoBehaviour
    {
        public Light fireLight;
        public ParticleSystem fireParticles;
        public TimeHandler timeHandler;
        [SerializeField]
        private int currentFireTime = 10;

        void Start()
        {
            timeHandler.OnHourChange += HandleFireTime;
        }

        private void HandleFireTime()
        {
            if (currentFireTime <= 0) {
                currentFireTime = 0;
                return;
            } 
            currentFireTime -= 1;
            if (currentFireTime == 0)
            {
                StopFire();
            }
        }

        public void StopFire()
        {
            fireLight.intensity = 0;
            fireParticles.Stop();
        }

        public void StartFire()
        {
            currentFireTime = 10;
            fireLight.intensity = 1;
            fireParticles.Play();
        }
    }
}