using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isAnimationPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void AnimateMotion(bool isMoving) {
        if (isAnimationPaused) return;
        animator.Play(isMoving ? "Walk" : "Idle");
    }

    public IEnumerator WaitForAnimation(string animationName)
    {
        isAnimationPaused =  true;
        // Play the animation
        animator.Play(animationName);

        // Wait for the current animation's duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Animation completed, resume here
        isAnimationPaused = false;
    }

    public void AnimateAction() {
        if (isAnimationPaused) return;
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            StartCoroutine(WaitForAnimation("Faint"));
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            StartCoroutine(WaitForAnimation("Head hit"));
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            StartCoroutine(WaitForAnimation("Picking up"));
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(WaitForAnimation("Jump"));
        }
    }
}
