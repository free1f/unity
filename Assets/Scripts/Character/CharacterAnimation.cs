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
        private AnimationData _animationData;
        
        public override void Init()
        {
            _animationData.animator = gameObject.GetComponent<Animator>();
        }

        private void AnimateMotion(bool isMoving) {
            if (_animationData.isAnimationPaused) return;
            _animationData.animator.Play(isMoving ? "Walk" : "Idle");
        }

        public void Tick()
        {
            AnimateMotion(_animationData.isMoving);
        }

        public void Attached(ref AnimationData value)
        {
            _animationData = value;
        }
    }
}