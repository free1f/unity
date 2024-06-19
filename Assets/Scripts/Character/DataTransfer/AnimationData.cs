using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Character.DataTransfer
{
    public class AnimationData
    {
        public bool isMoving;
        public bool isAnimationPaused;
        public Animator animator;
        
        public IEnumerator WaitForAnimation(string animationName, float timeForCallback = 0f, Action finishCallback = null)
        {
            isAnimationPaused =  true;
            // Play the animation
            animator.Play(animationName);

            // Wait for the current animation's duration
            yield return new WaitForSeconds(timeForCallback);
            finishCallback?.Invoke();
            
            yield return new WaitForSeconds(Mathf
                .Clamp(
                    animator.GetCurrentAnimatorStateInfo(0).length - timeForCallback,
                    0f, 
                    animator.GetCurrentAnimatorStateInfo(0).length)
            );
            
            
            // Animation completed, resume here
            isAnimationPaused = false;
        }
    }
}
