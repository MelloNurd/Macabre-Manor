using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Openable : MonoBehaviour
{
    Animator animator;
    public bool open = false;

    float slamSpeed = 4f;

    public AnimationClip openAnimation, closeAnimation;

    private bool isMoving = false;

    void Start()
    {
        animator = this.transform.GetComponent<Animator>();
    }

    public void ToggleOpen() {
        if (!isMoving) {
            if (open) Close();
            else Open();
        }
    }

    public void Open() {
        open = true;
        animator.Play(openAnimation.name);
        isMoving = true;
        StartCoroutine(AnimationEnd(openAnimation.length));
    }

    public void Close() {
        open = false;
        animator.Play(closeAnimation.name);
        isMoving = true;
        StartCoroutine(AnimationEnd(closeAnimation.length));
    }

    public void SlamClose() {
        animator.speed = slamSpeed;
        Close();
    }

    public void SlamOpen() {
        animator.speed = slamSpeed;
        Open();
    }

    IEnumerator AnimationEnd(float time) {
        yield return new WaitForSeconds(time);
        animator.speed = 1f;
        isMoving = false;
    }
}
