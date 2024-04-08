using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterAnimation : CharacterComponent, ITick, IAttached<AnimationData>
    {
        // private Animator animator;
        // private bool isAnimationPaused = false;
        private AnimationData Data;
        // public bool IsAnimationPaused { get => isAnimationPaused; }
        public override void Init()
        {
            Data.animator = gameObject.GetComponent<Animator>();
        }

        private void AnimateMotion(bool isMoving) {
            if (Data.isAnimationPaused) return;
            Data.animator.Play(isMoving ? "Walk" : "Idle");
        }

        // public IEnumerator WaitForAnimation(string animationName, Action finishCallback = null)
        // {
        //     isAnimationPaused =  true;
        //     // Play the animation
        //     animator.Play(animationName);

        //     // Wait for the current animation's duration
        //     // yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //     yield return new WaitForSeconds(2f);
        //     finishCallback?.Invoke();
        //     // Animation completed, resume here
        //     isAnimationPaused = false;
        // }

        public void Tick()
        {
            AnimateMotion(Data.isMoving);
        }

        public void Attached(ref AnimationData value)
        {
            Data = value;
        }
    }
}