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

    public bool isLocked = true;
    private bool isMoving = false;

    public bool canClose = true;
    public bool openOnUnlock = true;

    void Start()
    {
        //child = this.transform.GetChild(0).gameObject;
        animator = this.transform.GetComponent<Animator>();
    }

    public void Unlock() {
        Debug.Log("test");
        isLocked = false;
        gameObject.tag = "Openable";
        if (openOnUnlock) OpenClose();
    }

    public void OpenClose() {
        if (!isMoving && !isLocked) {
            if (open == false) {
                open = true;
                animator.Play(openAnimation.name);
                isMoving = true;
                StartCoroutine(AnimationEnd(openAnimation.length));
            }
            else if(canClose) {
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
