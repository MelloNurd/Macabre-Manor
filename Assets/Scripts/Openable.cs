using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Openable : MonoBehaviour
{
    Animator animator;
    public bool open = false;

    public AnimationClip openAnimation, closeAnimation;

    private bool isMoving = false;

    void Start()
    {
        animator = this.transform.GetComponent<Animator>();
    }

    public void OpenClose() {
        if (!isMoving) {
            if (open == false) {
                open = true;
                animator.Play(openAnimation.name);
                isMoving = true;
                StartCoroutine(AnimationEnd(openAnimation.length));
            }
            else {
                open = false;
                animator.Play(closeAnimation.name);
                isMoving = true;
                StartCoroutine(AnimationEnd(closeAnimation.length));
            }
        }
    }

    IEnumerator AnimationEnd(float time) {
        yield return new WaitForSeconds(time);
        isMoving = false;
    }
}
