using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterAnimation : CharacterComponent
    {
        private Animator animator;
        private bool isAnimationPaused = false;
        public bool IsAnimationPaused => isAnimationPaused;
        public override void Init()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public void AnimateMotion(bool isMoving) {
            if (isAnimationPaused) return;
            animator.Play(isMoving ? "Walk" : "Idle");
        }

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