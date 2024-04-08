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
        public IEnumerator WaitForAnimation(string animationName, Action finishCallback = null)
        {
            isAnimationPaused =  true;
            // Play the animation
            animator.Play(animationName);

            // Wait for the current animation's duration
            // yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(2f);
            finishCallback?.Invoke();
            // Animation completed, resume here
            isAnimationPaused = false;
        }
    }
}
